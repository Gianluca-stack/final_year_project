import pickle

# store label_dict in a pickle file
def store_label_encodings(label_dict):
    with open('data_volume/label_dict.pkl', 'wb') as f:
        pickle.dump(label_dict, f)

# load label_dict from a pickle file
def load_label_encodings():
    with open('C:\\Users\\gianl\\FYP\\Implementation\\VRLE\\Education\\Assets\\Python_scripts\\data_volume\\label_dict.pkl', 'rb') as f:
        label_dict = pickle.load(f)
    return label_dict\
    
    


