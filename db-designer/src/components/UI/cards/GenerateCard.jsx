import React, { useEffect, useState } from 'react';
import ComboBox from '../inputs/ComboBox';
import Button from '../buttons/Button';
import { getForCombobox } from '../../../utils/apiHelper';
import { getOrmForComboboxByLanguage } from '../../../services/ormService';
import { validateEmpty, validateNumber } from '../../../utils/validators';
import Checkbox from '../inputs/Checkbox';
import Input from '../inputs/Input';
import Loader from '../loaders/Loader';
import { download, generateDalAndTestData } from '../../../services/projectService';

const GenerateCard = ({projectId, dataBaseId, tables}) => {
  const [languageCombobox, setLanguageCombobox] = useState([]);
  const [language, setLanguage] = useState(0);
  const [languageError, setLanguageError] = useState('');
  
  const [ormCombobox, setOrmCombobox] = useState([]);
  const [orm, setOrm] = useState(0);
  const [ormError, setOrmError] = useState('');
  
  const [architectureCombobox, setArchitectureCombobox] = useState([]);
  const [architecture, setArchitecture] = useState(0);
  const [architectureError, setArchitectureError] = useState('');

  const [generationLanguageCombobox, setGenerationLanguageCombobox] = useState([]);
  const [generationLanguage, setGenerationLanguage] = useState(0);
  const [generationLanguageError, setGenerationLanguageError] = useState('');

  const [generationModelCombobox, setGenerationModelCombobox] = useState([]);
  const [generationModel, setGenerationModel] = useState(0);
  const [generationModelError, setGenerationModelError] = useState('');
  
  const [tableGenerateInfos, setTableGenerateInfos] = useState(tables.map(e => ({tableId: e.id, rowCount: '', isNeedToGenerate: false, error: ''})));
  const [errors, setErrors] = useState('');
  const [loading, setLoading] = useState(false);

  const [generateTestData, setGenerateTestData] = useState(false);
  const [generateDal, setGenerateDal] = useState(false);

  useEffect(() => {
    const fetchLanguages = async () => {
      const languages = await getForCombobox('Language');
      setLanguageCombobox(languages);
      const orms = await getForCombobox('Orm');
      setOrmCombobox(orms);
      const architectures = await getForCombobox('Architecture');
      setArchitectureCombobox(architectures);
      const generationLanguages = await getForCombobox('GenerationLanguage');
      setGenerationLanguageCombobox(generationLanguages);
      const generationModels = await getForCombobox('GenerationModel');
      setGenerationModelCombobox(generationModels);
    };

    fetchLanguages();
  }, []);

  async function handleLanguage(e) {
    setErrors('');
    setLanguageError('');
    setLanguage(e);
    if (e !== language) {
      const orms = await getOrmForComboboxByLanguage(e);
      setOrmCombobox(orms);
      setOrm(0);
    }
  }

  function handleOrm(e) {
    setErrors('');
    setOrmError('');
    setOrm(e);
  }

  function handleArchitecture(e) {
    setErrors('');
    setArchitectureError('');
    setArchitecture(e);
  }

  function handleGenerationLanguage(e) {
    setErrors('');
    setGenerationLanguageError('');
    setGenerationLanguage(e);
  }

  function handleGenerationModel(e) {
    setErrors('');
    setGenerationModelError('');
    setGenerationModel(e);
  }

  async function handleDownload() {
    setErrors('');
    setLoading(true);
    await download(projectId);
    setLoading(false);
  }

  function handleTableGenerateCount(tableId, rowCount){
    setErrors('');
    const validate = validateNumber(rowCount, tableId, 1, 100);
    if (!validate) {
      setTableGenerateInfos(tableGenerateInfos.map(e => {
        if (e.tableId == tableId) {
          e.error = '';
          e.rowCount = rowCount;
        }
        return e;
      }));
    }
  }

  function handleTableIsNeedToGenerate(tableId, value) {
    setErrors('');
    setTableGenerateInfos(tableGenerateInfos.map(e => {
      if (e.tableId == tableId) {
        e.error = '';
        e.isNeedToGenerate = value;
        if(!value){
          e.rowCount = '';
        }
      }
      return e;
    }));
  }

  async function handleSave() {
    setErrors('');
    const languageValidationError = validateEmpty(language, 'Language');
    const ormValidationError = validateEmpty(orm, 'Orm');
    const architectureValidationError = validateEmpty(architecture, 'Architecture');
    const generationLanguageValidationError = validateEmpty(generationLanguage, 'GenerationLanguage');
    const generationModelValidationError = validateEmpty(generationModel, 'GenerationModel');

    if (generateDal) {
      setLanguageError(languageValidationError);
      setOrmError(ormValidationError);
      setArchitectureError(architectureValidationError);
    }
    
    if (generateTestData) {
      setGenerationLanguageError(generationLanguageValidationError);
      setGenerationModelError(generationModelValidationError);
      setTableGenerateInfos(tableGenerateInfos.map(e => {
        if (e.isNeedToGenerate && !e.rowCount) {
          e.error = 'Count cannot be empty';
        }
        return e;
      }));
    }

    if ((generateDal && (languageValidationError || ormValidationError || architectureValidationError)) ||
      (generateTestData && (generationLanguageValidationError || generationModelValidationError) || tableGenerateInfos.some(e => e.isNeedToGenerate && !e.rowCount))) {
      return;
    }

    const model = {
      projectId: projectId,
      dataBaseId: dataBaseId,
      languageId: language,
      ormId: orm,
      architectureId: architecture,
      generationLanguageId: generationLanguage,
      generationModelId: generationModel,
      generateTestData: generateTestData,
      generateDal: generateDal,
      tableGenerateInfos: tableGenerateInfos.filter(e => e.isNeedToGenerate).map(e => ({tableId: e.tableId, rowCount: Number(e.rowCount)}))
    };

    setLoading(true);
    const result = await generateDalAndTestData(model);
    setErrors(result.errors);
    setLoading(false);
  }

  return (
    <div className="bg-gray-100 rounded p-2 my-1 shadow-md flex flex-col mx-auto w-[800px] max-h-[100vh] overflow-y-auto">
      <h2 className="text-2xl font-bold text-center mb-2">Generate options</h2>

      {loading
        ? (<Loader/>)
        : (
            <div className="flex flex-grow">
              <div className="w-1/2 pr-2">
                { generateDal ? (
                <>
                  <h2 className="text-xl mb-1 ml-1">Language</h2>
                  <ComboBox
                    options={languageCombobox}
                    selected={language}
                    onChange={handleLanguage}
                    placeholder="Select a Language"
                    error={languageError}
                    className="mb-1"
                  />

                  <h2 className="text-xl mb-1 ml-1">Orm</h2>
                  <ComboBox
                    options={ormCombobox}
                    selected={orm}
                    onChange={handleOrm}
                    placeholder="Select an Orm"
                    error={ormError}
                    className="mb-1"
                  />

                  <h2 className="text-xl mb-1 ml-1">Architecture</h2>
                  <ComboBox
                    options={architectureCombobox}
                    selected={architecture}
                    onChange={handleArchitecture}
                    placeholder="Select an Architecture"
                    error={architectureError}
                    className="mb-1"
                  />
                </>
                ) : <></>}

                { generateTestData ? (
                <>
                  <h2 className="text-xl mb-1 ml-1">Language for generation</h2>
                  <ComboBox
                    options={generationLanguageCombobox}
                    selected={generationLanguage}
                    onChange={handleGenerationLanguage}
                    placeholder="Select an language"
                    error={generationLanguageError}
                    className="mb-1"
                  />

                  <h2 className="text-xl mb-1 ml-1">Model for generation</h2>
                  <ComboBox
                    options={generationModelCombobox}
                    selected={generationModel}
                    onChange={handleGenerationModel}
                    placeholder="Select an model"
                    error={generationModelError}
                    className="mb-1"
                  />
                </>
                ) : <></>}

                <Checkbox
                  label={"Generate DAL"}
                  checked={generateDal}
                  onChange={(e) => setGenerateDal(e.target.checked)}
                  className="mb-1"
                />

                <Checkbox
                  label={"Generate test data"}
                  checked={generateTestData}
                  onChange={(e) => setGenerateTestData(e.target.checked)}
                  className="mb-1"
                />
              </div>

              <div className="w-1/2 pl-2">
                {(generateTestData ? tables : []).map((table) => {
                  const tableGenerateInfo = tableGenerateInfos.find(e => e.tableId === table.id);
                  return (<div key={table.id}>
                    <h2 className="text-xl mb-1 ml-1">{table.name}</h2>
                    <div className="flex items-center w-full space-x-2">
                      <div className="flex-grow">
                        <Input
                          placeholder="Enter generate count"
                          type="number"
                          disabled={!tableGenerateInfo.isNeedToGenerate}
                          className="w-full mb-1"
                          value={tableGenerateInfo.rowCount}
                          error={tableGenerateInfo.error}
                          onChange={(e) => handleTableGenerateCount(table.id, e.target.value)}
                        />
                      </div>
                      <div className="w-12">
                        <Checkbox
                          onChange={(e) => handleTableIsNeedToGenerate(table.id, e.target.checked)}
                          checked={tableGenerateInfo.isNeedToGenerate}
                          className="w-full"/>
                      </div>
                    </div>
                  </div>
                  )})}
              </div>
            </div>
          )}

      {loading
      ? <></>
      : <div className="flex justify-end mt-2 gap-1">
        {errors == null
          ? (
            <Button onClick={handleDownload}>
              <i className="fa-solid fa-download"></i>
            </Button>
          ) : <></>}
          <Button onClick={handleSave}>
            <i className="fa-solid fa-cloud-arrow-up"></i>
          </Button>
        </div>
      }

      {errors ? (
        <div className="mt-2 p-2 bg-red-100 border border-red-400 text-red-700 rounded">
          {errors}
        </div>
      ) : errors == null ? (
        <div className="mt-2 p-2 bg-green-100 border border-green-400 text-green-700 rounded">
          The generation was successful.
        </div>
      ) : null}
    </div>
  );
};

export default GenerateCard;