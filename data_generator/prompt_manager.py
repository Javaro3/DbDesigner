from models import PromptModel, DataGeneratorRequestDto, ColumnDto

class prompt_manager:
  def generate_prompt(self, model: PromptModel):
    return f'''
You are an expert SQL data generator specializing in {model.database}. Generate ONLY a clean, properly formatted INSERT statement that meets these strict requirements:

1. Language: {model.language} only
2. Data Quality: 
  - Realistic, production-grade data
  - Natural variation in values
  - Properly formatted for each data type
  - Contextually appropriate values that relate logically
3. Output Format:
  - ONLY the pure {model.database} INSERT statement
  - Single INSERT with multiple VALUES clauses
  - Perfect vertical alignment
  - Consistent indentation (4 spaces)
4. Quantity: Exactly {model.rowCount} records
5. Table name: {model.tableName}
5. Field Rules:
{model.fields}
6. Special Requirements:
  - Maintain referential integrity if foreign keys exist
  - Follow {model.database} datetime format
  - Escape special characters properly

Example output for table 'Products' with fields (id, name, price):
INSERT INTO Products (id, name, price)
VALUES
    (1, 'Wireless Headphones', 129.99),
    (2, 'Bluetooth Speaker', 79.95),
    (3, 'USB-C Cable', 12.50),
    (4, NULL, 5.99),
    (5, 'Smartphone Case', 24.99);

Important: Output ONLY the INSERT statement with no additional commentary, explanations or markdown formatting.
    '''
  
  def create_prompt_models(self, data: DataGeneratorRequestDto):
    models = []

    for table in data.tableGenerateInfos:
      tableModel = [t for t in data.project.tables if t.id == table.tableId][0]
      models.append(
        PromptModel(
          database=data.project.dataBase.name,
          language=data.generationLanguage,
          rowCount=table.rowCount,
          tableName=tableModel.name,
          fields="\n".join([self.field_generator(data, column) for column in tableModel.columns])
      ))

    return models

  def field_generator(self, data: DataGeneratorRequestDto, column: ColumnDto):
    result = f"  - {column.name} {column.sqlTypeName}"
    if column.sqlTypeHasParams:
      result += f"({column.sqlTypeParams})"
    
    for property in column.properties:
      prop = f" {property.propertyName}"
      if property.propertyHasParams:
        property += f" {property.propertyParams}"
      result += prop

    primary_table_id = [relation.sourceColumn.tableId for relation in data.project.relations if relation.targetColumnId == column.id]
    if len(primary_table_id) > 0:
      max_value = [table.rowCount for table in data.tableGenerateInfos if table.tableId == primary_table_id[0]]
      if len(max_value) > 0:
        result += f" (the value must be in the range from 1 to {max_value[0]})"

    return result