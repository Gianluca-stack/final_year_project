from openai import OpenAI
import sys 
import csv


def main():
    # Get command-line arguments
    args = sys.argv[1:]  # Exclude the first argument, which is the script name

    # Parse arguments
    if len(args) != 2:
        print("Usage: python Model.py <number1> <number2>")
        return

    object_name = args[0]
    difficulty = args[1]

    # variable for .csv data
    data = []
    prompt = ""

    with open('C:\\Users\\gianl\\FYP\\Implementation\\VRLE\\Education\\Assets\\trees_and_plants_information.csv', newline='') as csvfile:
        csv_data = csv.reader(csvfile, delimiter=',')

        for row in csv_data:
            data.append(row)

    # check in data if object_name exists and get the information
    for row in data:
        if row[0] == object_name:
            temp_data = row
            # get difficulty index
            if difficulty == "Easy":
                prompt = temp_data[2]
            elif difficulty == "Medium":
                prompt = temp_data[3]
            elif difficulty == "Hard":
                prompt = temp_data[4]
            break

    client = OpenAI(api_key="sk-proj-rpoxxsraS6SAUCU5WqlJT3BlbkFJnatQH0xUxuIRP9dqNemW")

    response = client.chat.completions.create(
      model="gpt-3.5-turbo",
      messages=[
        {
          "role": "system",
          "content": "Hello"
        },
        {
          "role": "user",
          "content": prompt
        }
      ]
    )

    print(response.choices[0].message.content)

if __name__ == "__main__":
    main()