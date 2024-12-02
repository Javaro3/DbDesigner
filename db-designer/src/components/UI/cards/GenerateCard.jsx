import React, { useEffect, useState } from 'react';
import ComboBox from '../inputs/ComboBox';
import Button from '../buttons/Button';
import { getForCombobox } from '../../../utils/apiHelper';
import { getOrmForComboboxByLanguage } from '../../../services/ormService';
import { validateEmpty, validateNumber } from '../../../utils/validators';
import Checkbox from '../inputs/Checkbox';
import Input from '../inputs/Input';
import { generate } from '../../../services/projectService';
import Loader from '../loaders/Loader';

const GenerateCard = ({projectId, dataBaseId, tables}) => {
  const [languageCombobox, setLanguageCombobox] = useState([]);
  const [language, setLanguage] = useState(0);
  const [languageError, setLanguageError] = useState('');
  const [loading, setLoading] = useState(false);

  const [ormCombobox, setOrmCombobox] = useState([]);
  const [orm, setOrm] = useState(0);
  const [ormError, setOrmError] = useState('');

  const [architectureCombobox, setArchitectureCombobox] = useState([]);
  const [architecture, setArchitecture] = useState(0);
  const [architectureError, setArchitectureError] = useState('');

  const [tableGenerateInfos, setTableGenerateInfos] = useState(tables.map(e => ({tableId: e.id, rowCount: '', isNeedToGenerate: false, error: ''})));

  useEffect(() => {
    const fetchLanguages = async () => {
      const languages = await getForCombobox('Language');
      setLanguageCombobox(languages);
      const orms = await getForCombobox('Orm');
      setOrmCombobox(orms);
      const architectures = await getForCombobox('Architecture');
      setArchitectureCombobox(architectures);
    };

    fetchLanguages();
  }, []);

  async function handleLanguage(e) {
    setLanguageError('');
    setLanguage(e);
    if (e !== language) {
      const orms = await getOrmForComboboxByLanguage(e);
      setOrmCombobox(orms);
      setOrm(0);
    }
  }

  function handleOrm(e) {
    setOrmError('');
    setOrm(e);
  }

  function handleArchitecture(e) {
    setArchitectureError('');
    setArchitecture(e);
  }

  function handleTableGenerateCount(tableId, rowCount){
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
    const languageValidationError = validateEmpty(language, 'Language');
    const ormValidationError = validateEmpty(orm, 'Orm');
    const architectureValidationError = validateEmpty(architecture, 'Architecture');

    setLanguageError(languageValidationError);
    setOrmError(ormValidationError);
    setArchitectureError(architectureValidationError);

    setTableGenerateInfos(tableGenerateInfos.map(e => {
      if (e.isNeedToGenerate && !e.rowCount) {
        e.error = 'Count cannot be empty';
      }
      return e;
    }));

    if (languageValidationError ||
      ormValidationError ||
      architectureValidationError ||
      tableGenerateInfos.some(e => e.isNeedToGenerate && !e.rowCount)) {
      return;
    }

    const model = {
      projectId: projectId,
      dataBaseId: dataBaseId,
      languageId: language,
      ormId: orm,
      architectureId: architecture,
      tableGenerateInfos: tableGenerateInfos.filter(e => e.isNeedToGenerate).map(e => ({tableId: e.tableId, rowCount: e.rowCount}))
    };

    setLoading(true);
    await generate(model, 'result.zip');
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
              </div>

              <div className="w-1/2 pl-2">
                {tables.map((table) => {
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
      : <div className="flex justify-end mt-2">
          <Button onClick={handleSave}>
            <i className="fa-solid fa-cloud-arrow-up"></i>
          </Button>
        </div>
      }
    </div>
  );
};

export default GenerateCard;