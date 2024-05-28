import pandas as pd

import torch

from store_label_encodings import *

from tqdm import tqdm

from transformers import BertTokenizer, BertModel

from torch.utils.data import TensorDataset

from transformers import BertForSequenceClassification

from sklearn.model_selection import train_test_split

from torch.utils.data import DataLoader, RandomSampler, SequentialSampler

from transformers import AdamW, get_linear_schedule_with_warmup

from sklearn.metrics import confusion_matrix, f1_score

import numpy as np

import random

import warnings

import os

import itertools

import matplotlib.pyplot as plt



# Suppress the FutureWarning for `resume_download`
warnings.filterwarnings("ignore", message="`resume_download` is deprecated and will be removed in version 1.0.0. Downloads always resume when possible. If you want to force a new download, use `force_download=True`.")
label_dict = load_label_encodings()

def load_csv_file(csv_file):
    # Load the CSV file
    df = pd.read_csv(csv_file)
    return df

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
    



# plot a confusion matrix
def plot_confusion_matrix(cm, classes, normalize=False, title='Confusion matrix', cmap=plt.cm.Blues):
    """
    This function prints and plots the confusion matrix.
    Normalization can be applied by setting `normalize=True`.
    """
    if normalize:
        cm = cm.astype('float') / cm.sum(axis=1)[:, np.newaxis]
        print("Normalized confusion matrix")
    else:
        print('Confusion matrix, without normalization')

    print(cm)

    plt.imshow(cm, interpolation='nearest', cmap=cmap)
    plt.title(title)
    plt.colorbar()
    tick_marks = np.arange(len(classes))
    plt.xticks(tick_marks, classes, rotation=45)
    plt.yticks(tick_marks, classes)

    fmt = '.2f' if normalize else 'd'
    thresh = cm.max() / 2.
    for i, j in itertools.product(range(cm.shape[0]), range(cm.shape[1])):
        plt.text(j, i, format(cm[i, j], fmt),
                 horizontalalignment="center",
                 color="white" if cm[i, j] > thresh else "black")

    plt.ylabel('True label')
    plt.xlabel('Predicted label')
    plt.tight_layout()





csv_file = 'C:\\Users\\gianl\\FYP\\Implementation\\VRLE\\Education\\Assets\\Python_scripts\\human_1.csv'
# csv_file = 'C:\\Users\\gianl\\FYP\\Implementation\\VRLE\\Education\\Assets\\Python_scripts\\human_2.csv'
gardening_label_count = 0
computing_label_count = 0
physics_label_count = 0
# if path_to_save_predictions.csv exists, delete it
if os.path.exists('path_to_save_predictions.csv'):
    os.remove('path_to_save_predictions.csv')
else:
    pass

tokenizer = BertTokenizer.from_pretrained('bert-base-uncased', do_lower_case=True)

test_df = pd.read_csv(csv_file)

test_df['label'] = test_df.Subject.replace(label_dict)

encoded_data_test = tokenizer.batch_encode_plus(
    test_df.Text.values,
    add_special_tokens=True,
    return_attention_mask=True,
    padding=True,
    truncation=True,
    return_tensors='pt'
)

input_ids_test = encoded_data_test['input_ids']
attention_masks_test = encoded_data_test['attention_mask']
labels_test = torch.tensor(test_df.label.values)

dataset_test = TensorDataset(input_ids_test, attention_masks_test, labels_test)

batch_size = 3

dataloader_test = DataLoader(
    dataset_test,
    sampler=SequentialSampler(dataset_test),
    batch_size=batch_size
)

model = BertForSequenceClassification.from_pretrained('bert-base-uncased', num_labels=len(label_dict), output_attentions=False, output_hidden_states=False)

# Set up the device
device = torch.device('cuda' if torch.cuda.is_available() else 'cpu')
model.to(device)  # Move the model to the device

# Load the saved model state
model.load_state_dict(torch.load('C:\\Users\\gianl\\FYP\\Implementation\\VRLE\\Education\\Assets\\Python_scripts\\data_volume\\finetuned_BERT_epoch_10.model', map_location=device))

model.eval()

predictions = []

# Deactivate autograd for evaluation
with torch.no_grad():
    for batch in dataloader_test:
        batch = tuple(t.to(device) for t in batch)
        inputs = {'input_ids': batch[0],
                'attention_mask': batch[1],
                'labels': batch[2]}
        
        outputs = model(**inputs)
        logits = outputs[1]
        logits = logits.detach().cpu().numpy()
        predictions.append(logits)

# Concatenate predictions from all batches
predictions = np.concatenate(predictions, axis=0)

# Convert logits to predicted class labels
predicted_labels = np.argmax(predictions, axis=1)

# Map predicted labels to class names
predicted_classes = [list(label_dict.keys())[list(label_dict.values()).index(label)] for label in predicted_labels]

# Add the predictions to the test dataframe
test_df['predicted_subject'] = predicted_classes

# Save the predictions to a CSV file
test_df.to_csv('path_to_save_predictions.csv', index=False)

# Calculate the F1 score
f1_score = f1_score_func(predictions, labels_test)

# Calculate the accuracy per class
accuracy_per_class(predictions, labels_test)

# load the CSV file
df = load_csv_file(csv_file)

# get each row's label and if it is Gardening, Computing or Physics increment the respective counter
for row in df.iterrows():
    if row[1]['Subject'] == 'Gardening':
        gardening_label_count += 1
    elif row[1]['Subject'] == 'Computing':
        computing_label_count += 1
    elif row[1]['Subject'] == 'Physics':
        physics_label_count += 1

# find from the three counters which has the highest value
preferred_subject = max(gardening_label_count, computing_label_count, physics_label_count)

# return the preferred subject as a string
if preferred_subject == gardening_label_count:
    print('Gardening')
elif preferred_subject == computing_label_count:
    print('Computing')
elif preferred_subject == physics_label_count:
    print('Physics')
else:
    print('No preferred subject')

# plot a confusion matrix
cm = confusion_matrix(labels_test, predicted_labels)
plot_confusion_matrix(cm, label_dict.keys(), normalize=True)
plt.show()