from g4f.client import Client
import json

table_name = "AppleSort"
fields = ['Name', 'Price']
language = "Русский"
count = 10

fields_str = ""
for i in range(len(fields)):
    fields_str += f"'{fields[i]}'{',' if i != len(fields)-1 else ''}\n"

query = f'''
Ты генератор фэйковых данных для БД.
Все данные должны быть на языке {language}
Сгенерирую {count} записей для таблицы '{table_name}' если она содержит поля: 
{fields_str}
Напиши только json файл который содержит сгенерированные данные и больше ни чего больше.
json должен иметь формат:
[
    {{
        "Field1": "Value11",
        "Field2": "Value12"
    }},
    {{
        "Field1": "Value21",
        "Field2": "Value22"
    }},
]
И не надо обрачивать его в {{'{table_name}': []}}
'''

print(f"table_name:\n{table_name}\n")
print(f"fields:\n{fields_str}\n")
print(f"language:\n{language}\n")
print(f"count:\n{count}\n\n")
print(f"query:\n{query}")

client = Client()
response = client.chat.completions.create(
    model="gpt-4o",
    messages=[{"role": "user", "content": query}],
)
json_string = response.choices[0].message.content.removeprefix("```json\n").removesuffix("```")
print(json_string)
data = json.loads(json_string)
print(data)
print(len(data))