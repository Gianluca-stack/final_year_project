import csv
import requests
from bs4 import BeautifulSoup
import re
import os
import nltk
from nltk.corpus import stopwords
import spacy

def ui():
    print('Welcome to the web scraping tool')
    print("---------------------------------")
    print("1. Web Scraping")
    print("2. Print Number of Labels + Check Data")
    print("3. Remove Row")
    print("4. Read CSV File")
    print("5. Exit")
    print("---------------------------------")

def clean_text(text):
    # Remove leading/trailing whitespace
    text = text.strip()
    # Remove multiple spaces, newlines, and tabs
    text = re.sub(r'\s+', ' ', text)
    # Remove non-ASCII characters 
    text = text.encode('ascii', errors='ignore').decode()
    # Remove URLs
    text = re.sub(r'http\S+', '', text)
    # Remove email addresses
    text = re.sub(r'\S+@\S+', '', text)
    # Remove special characters
    text = re.sub(r'[^A-Za-z0-9 ]+', '', text)
    # Lowercase the text
    text = text.lower()

    # Lemmatize the text

    return text

# remove row function
def remove_row(csv_file, row_number):
    # Read the CSV file
    with open(csv_file, 'r') as file:
        # Create a CSV reader object
        csv_reader = csv.reader(file)
        # Convert the reader object to a list
        rows = list(csv_reader)

    # Remove the row
    rows.pop(row_number)

    # Write the rows to the CSV file
    with open(csv_file, 'w', newline='') as file:
        # Create a CSV writer object
        csv_writer = csv.writer(file)
        # Write the rows to the CSV file
        csv_writer.writerows(rows)

    print('Row removed successfully')

# get the text of each row except the first row and check if the text is different
def check_data(csv_file):
    # Open the CSV file
    with open(csv_file, 'r') as file:
        # Create a CSV reader object
        csv_reader = csv.reader(file)

        # Skip the header row
        next(csv_reader)

        # Get the text of each row
        text_data = [row[0] for row in csv_reader]

        # Check if the text is different
        if len(set(text_data)) == 1:
            print('All text data is the same')
        else:
            print('Text data is different')

# print dataset labels of each row except the first row
def print_labels(csv_file):
    label_count = 0
    gardening_label_count = 0
    computing_label_count = 0
    physics_label_count = 0
    # Open the CSV file
    with open(csv_file, 'r') as file:
        # Create a CSV reader object
        csv_reader = csv.reader(file)

        # Skip the header row
        next(csv_reader)

        # Iterate over each row in the CSV file
        for row in csv_reader:
            label_count += 1
            if row[1] == 'Gardening':
                gardening_label_count += 1
            elif row[1] == 'Computing':
                computing_label_count += 1
            elif row[1] == 'Physics':
                physics_label_count += 1
        
    print(f'Total number of labels: {label_count} \nGardening: {gardening_label_count} \nComputing: {computing_label_count} \nPhysics: {physics_label_count}')

def read_csv_file(csv_file):
    # Open the CSV file
    with open(csv_file, 'r') as file:
        # Create a CSV reader object
        csv_reader = csv.reader(file)

        # Iterate over each row in the CSV file
        for row in csv_reader:
            # Print each row
            print(row)

def store_csv_file(csv_file, text_data, label_data):
    # Ensure the directory exists
    directory = os.path.dirname(csv_file)
    if not os.path.exists(directory):
        os.makedirs(directory)

    # Open the CSV file in append mode
    with open(csv_file, 'a', newline='') as file:
        # Create a CSV writer object
        csv_writer = csv.writer(file)

        # If single string, convert to list
        if isinstance(text_data, str):
            text_data = [text_data]
        if isinstance(label_data, str):
            label_data = [label_data]

        # Ensure label_data has the same length as text_data
        if len(label_data) == 1:
            label_data = label_data * len(text_data)

        # Iterate over each text and label
        for text, label in zip(text_data, label_data):
            # Write the text and label to the CSV file
            csv_writer.writerow([text, label])

    print('Data stored successfully')

def web_scraping(csv_file, url):
    # Specify the URL
    inputed_url = url

    # Use the requests library to send an HTTP request to the URL
    headers = {'User-Agent': 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36'}
    response = requests.get(inputed_url, headers=headers)

    # Check if the request was successful
    if response.status_code == 200:
        # Parse the HTML content of the page
        soup = BeautifulSoup(response.text, 'html.parser')

        # Find all the <p> tags within <article> tags, if there is no <article> tag, find all <p> tags
        article = soup.find('article')
        if article:
            paragraphs = article.find_all('p')
        else:
            paragraphs = soup.find_all('p')

        # Extract the text from the paragraphs
        text_data = [paragraph.get_text() for paragraph in paragraphs]
        if not text_data:
            print('No text data found')
            return
        # Clean the text data
        cleaned_text_data = [clean_text(text) for text in text_data]

        # Combine all cleaned text data into a single string
        combined_text = ' '.join(cleaned_text_data)

        label = 'Physics'

        # Store the combined text and label data in a CSV file
        store_csv_file(csv_file, combined_text, label)
    else:
        # Print an error message with the status code
        print(f'Error: Unable to fetch the URL. Status code: {response.status_code}')

# check data is different

# Specify the path to your CSV file
csv_file = 'C:\\Users\\gianl\\FYP\\Implementation\\VRLE\\Education\\Assets\\Python_scripts\\human_1.csv'

ui()

# Ask the user to input the option
option = input("Enter the option: ")
# do the following until the user enters 5
while option != '5':
    if option == '1':
        url = input("Enter the URL: ")
        web_scraping(csv_file, url)
    elif option == '2':
        print_labels(csv_file)
        check_data(csv_file)
    elif option == '3':
        row_number = int(input("Enter the row number to remove: "))
        remove_row(csv_file, row_number)
    elif option == '4':
        read_csv_file(csv_file)
    else:
        print("Invalid option")
    print()
    ui()
    option = input("Enter the option: ")
