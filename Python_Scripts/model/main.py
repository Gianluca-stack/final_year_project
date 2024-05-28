import pandas as pd

import torch
from tqdm import tqdm

from transformers import BertTokenizer, BertModel
from torch.utils.data import TensorDataset

from transformers import BertForSequenceClassification

from sklearn.model_selection import train_test_split

from torch.utils.data import DataLoader, RandomSampler, SequentialSampler

from transformers import AdamW, get_linear_schedule_with_warmup

from sklearn.metrics import f1_score

import numpy as np

import random

import warnings

import os

# Suppress the FutureWarning for `resume_download`
warnings.filterwarnings("ignore", message="`resume_download` is deprecated and will be removed in version 1.0.0. Downloads always resume when possible. If you want to force a new download, use `force_download=True`.")
csv_file = 'C:\\Users\\gianl\\FYP\\Implementation\\VRLE\\Education\\Assets\\Python_scripts\\fyp_dataset.csv'
label_dict = {}
seed_val = 17
random.seed(seed_val)
np.random.seed(seed_val)
torch.manual_seed(seed_val)
torch.cuda.manual_seed_all(seed_val)

def encoding_labels(passed_labels):
    temp_dict = {}
    for index, passed_labels in enumerate(passed_labels):
        temp_dict[passed_labels] = index
    
    return temp_dict

# Sliding window tokenize function
# def sliding_window_tokenize(text, tokenizer, max_length, stride):
#     tokens = tokenizer.encode(text, add_special_tokens=True)
#     segments = []
#     for i in range(0, len(tokens), stride):
#         segment = tokens[i:i + max_length]
#         if len(segment) < max_length:
#             segment = segment + [tokenizer.pad_token_id] * (max_length - len(segment))
#         segments.append(segment)
#         if len(segment) < max_length:
#             break
#     return segments

def f1_score_func(preds, labels):
    preds_flat = np.argmax(preds, axis=1).flatten()
    labels_flat = labels.flatten()
    return f1_score(labels_flat, preds_flat, average='weighted')

def accuracy_per_class(preds, labels):
    label_dict_inverse = {v: k for k, v in label_dict.items()}
    
    preds_flat = np.argmax(preds, axis=1).flatten()
    labels_flat = labels.flatten()

    for label in np.unique(labels_flat):
        y_preds = preds_flat[labels_flat==label]
        y_true = labels_flat[labels_flat==label]
        print(f'Class: {label_dict_inverse[label]}')
        print(f'Accuracy: {len(y_preds[y_preds==label])}/{len(y_true)}\n')

def evaluate(dataloader_val):

    model.eval()
    
    loss_val_total = 0
    predictions, true_vals = [], []
    
    for batch in dataloader_val:
        
        batch = tuple(b.to(device) for b in batch)
        
        inputs = {'input_ids':      batch[0],
                  'attention_mask': batch[1],
                  'labels':         batch[2],
                 }

        with torch.no_grad():        
            outputs = model(**inputs)
            
        loss = outputs[0]
        logits = outputs[1]
        loss_val_total += loss.item()

        logits = logits.detach().cpu().numpy()
        label_ids = inputs['labels'].cpu().numpy()
        predictions.append(logits)
        true_vals.append(label_ids)
    
    loss_val_avg = loss_val_total/len(dataloader_val) 
    
    predictions = np.concatenate(predictions, axis=0)
    true_vals = np.concatenate(true_vals, axis=0)
            
    return loss_val_avg, predictions, true_vals

def validate_model():
    model = BertForSequenceClassification.from_pretrained("bert-base-uncased",
                                                      num_labels=len(label_dict),
                                                      output_attentions=False,
                                                      output_hidden_states=False)

    model.to(device)

    model.load_state_dict(torch.load('data_volume/finetuned_BERT_epoch_10.model', map_location=torch.device('cpu')))

    _, predictions, true_vals = evaluate(dataloader_val)
    accuracy_per_class(predictions, true_vals)

    return model

# Read the CSV file
df = pd.read_csv(csv_file)

# store the possible labels in a list
possible_labels = df.Subject.unique()

# Encode the labels
label_dict = encoding_labels(possible_labels)

# Replace the labels with the encoded labels
df['label'] = df.Subject.replace(label_dict)

# Split the data into training and testing sets, percanatge of 85% training and 15% testing
X_train, X_val, y_train, y_val = train_test_split(df.index.values, 
                                                  df.label.values, 
                                                  test_size=0.15, 
                                                  random_state=42, 
                                                  stratify=df.label.values)

# Add a column to the dataframe to indicate the data split
df['data_type'] = ['not_set']*df.shape[0]

# Set the data type for the training and validation sets
df.loc[X_train, 'data_type'] = 'train'
df.loc[X_val, 'data_type'] = 'val'

# groupby
# print(df.groupby(['Subject', 'label', 'data_type']).count())

# Load the pre-trained BERT model
tokenizer = BertTokenizer.from_pretrained('bert-base-uncased', 
                                          do_lower_case=True)


# Encode the data
encoded_data_train = tokenizer.batch_encode_plus(
    df[df.data_type=='train'].Text.values, 
    add_special_tokens=True, 
    return_attention_mask=True, 
    padding=True,
    max_length=512, 
    truncation=True,
    return_tensors='pt'
)

encoded_data_val = tokenizer.batch_encode_plus(
    df[df.data_type=='val'].Text.values, 
    add_special_tokens=True, 
    return_attention_mask=True, 
    padding=True, 
    max_length=512, 
    truncation=True,
    return_tensors='pt'
)


input_ids_train = encoded_data_train['input_ids']
attention_masks_train = encoded_data_train['attention_mask']
labels_train = torch.tensor(df[df.data_type=='train'].label.values)

input_ids_val = encoded_data_val['input_ids']
attention_masks_val = encoded_data_val['attention_mask']
labels_val = torch.tensor(df[df.data_type=='val'].label.values)

dataset_train = TensorDataset(input_ids_train, attention_masks_train, labels_train)
dataset_val = TensorDataset(input_ids_val, attention_masks_val, labels_val)

# Create a dataloader
batch_size = 3

# Assuming `dataset_train` and `dataset_val` are already created properly
dataloader_train = DataLoader(dataset_train, sampler=RandomSampler(dataset_train), batch_size=batch_size)
dataloader_val = DataLoader(dataset_val, sampler=SequentialSampler(dataset_val), batch_size=batch_size)

# Load the pre-trained BERT model
model = BertForSequenceClassification.from_pretrained("bert-base-uncased",
                                                      num_labels=len(label_dict),
                                                      output_attentions=False,
                                                      output_hidden_states=False)

# Set the optimizer using PyTorch's AdamW
optimizer = torch.optim.AdamW(model.parameters(), lr=1e-5, eps=1e-8)

# Set up the device
device = torch.device('cuda' if torch.cuda.is_available() else 'cpu')
model.to(device)  # Move the model to the device

# Set the optimizer using PyTorch's AdamW, learning rate of 1e-5, 1e-5 equals 0.00001 and eps of 1e-8 equals 0.00000001
optimizer = torch.optim.AdamW(model.parameters(), lr=1e-5, eps=1e-8)

# Set the scheduler
epochs = 10
scheduler = get_linear_schedule_with_warmup(optimizer,
                                            num_warmup_steps=0,
                                            num_training_steps=len(dataloader_train) * epochs)

# Create the directory if it doesn't exist
os.makedirs('data_volume', exist_ok=True)

# Training loop
for epoch in tqdm(range(1, epochs + 1), desc="Epochs"):
    model.train()
    loss_train_total = 0
    progress_bar = tqdm(dataloader_train, desc='Epoch {:1d}'.format(epoch), leave=False, disable=False)
    for batch in progress_bar:
        model.zero_grad()
        
        # Move batch to device
        batch = tuple(b.to(device) for b in batch)
        
        inputs = {'input_ids': batch[0],
                  'attention_mask': batch[1],
                  'labels': batch[2]}
        
        outputs = model(**inputs)
        loss = outputs.loss
        loss_train_total += loss.item()
        loss.backward()
        torch.nn.utils.clip_grad_norm_(model.parameters(), 1.0)
        optimizer.step()
        scheduler.step()
        progress_bar.set_postfix({'training_loss': '{:.3f}'.format(loss.item() / len(batch))})

    torch.save(model.state_dict(), f'data_volume/finetuned_BERT_epoch_{epoch}.model')
    tqdm.write(f'\nEpoch {epoch}')
    loss_train_avg = loss_train_total / len(dataloader_train)
    tqdm.write(f'Training loss: {loss_train_avg}')
    val_loss, predictions, true_vals = evaluate(dataloader_val)
    val_f1 = f1_score_func(predictions, true_vals)
    tqdm.write(f'Validation loss: {val_loss}')
    tqdm.write(f'F1 Score (Weighted): {val_f1}')




