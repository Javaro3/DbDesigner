import React, { useState } from 'react';
import Editor from '@monaco-editor/react';
import { format } from 'sql-formatter';
import Button from '../buttons/Button';
import { downloadScript, generateScript, scriptIsValid } from '../../../services/projectService';
import Loader from '../loaders/Loader';

function SqlScriptEditorCard({ projectId, openGenerateCard }) {
    const [script, setScript] = useState('');
    const [theme, setTheme] = useState('vs-dark');
    const [errors, setErrors] = useState('');
    const [isValid, setIsValid] = useState(false);
    const [loading, setLoading] = useState(false);

    const handleEditorChange = (value) => {
      setScript(value);
      if (isValid)
        setIsValid(false);
    };

    const handleFormat = () => {
        const formattedScript = format(script, { language: 'sql' });
        setScript(formattedScript);
    };

    const handleThemeChange = () => {
        setTheme(theme === 'vs-dark' ? 'vs-light' : 'vs-dark');
    };

    const handleGenerate = async () => {
      setLoading(true);
      const data = await generateScript(projectId);
      setScript(data.script);
      setErrors(data.errors || '');
      setIsValid(data.errors === null);
      setLoading(false);
    };

    const handleIsValid = async () => {
      setLoading(true);
      const result = await scriptIsValid({script, projectId});
      setErrors(result.errors || '');
      setIsValid(result.errors === null);
      setLoading(false);
    };

    const handleScriptDownload = async () => {
      setLoading(true);
      try {
        await downloadScript(projectId, 'script.sql');
      }
      catch (ex) {
        setErrors(ex.message);
        setIsValid(false);
      } 
      finally {
        setLoading(false);
      }
    };

    return (
        <div style={{ width: '1200px', height: '800px'}}>
            { loading
            ? (<Loader/>)
            : (
            <>
              <div className='my-1'>
                <Button size='small' className='mr-1' onClick={handleThemeChange}>
                    {theme === 'vs-dark'
                      ? <i className="fa-solid fa-sun"></i>
                      : <i className="fa-solid fa-moon"></i>
                    }
                </Button>
                <Button className='mr-1' size='small' onClick={handleFormat}>
                  <i className="fa-solid fa-code"></i>
                </Button>
                <Button className='mr-1' size='small' onClick={handleGenerate}>
                  <i className="fa-solid fa-upload"></i>
                </Button>
                <Button className='mr-1' size='small' variant={isValid ? 'primary' : 'danger'} onClick={handleIsValid}>
                  <i className="fa-solid fa-vial-circle-check"></i>
                </Button>
                <Button className='mr-1' size='small' onClick={handleScriptDownload} disabled={!isValid}>
                  <i className="fa-solid fa-download"></i>
                </Button>
                <Button className='mr-1' size='small' onClick={openGenerateCard} disabled={!isValid}>
                  <i className="fa-solid fa-right-long"></i>
                </Button>
              </div>
              <div className="grid grid-rows-[1fr_auto] h-[96%]">
                <div className="rounded-lg overflow-hidden border border-gray-300">
                    <Editor
                        height="100%"
                        width="100%"
                        language="sql"
                        value={script}
                        onChange={handleEditorChange}
                        theme={theme}
                        options={{
                            lineNumbersMinChars: 2,
                            fontSize: 16,
                            minimap: { enabled: false },
                            wordWrap: 'on',
                            autoClosingBrackets: 'always',
                            autoClosingQuotes: 'always',
                            suggest: {
                                showKeywords: true,
                                showSnippets: true,
                            },
                        }}
                    />
                </div>
                {errors ? (
                    <div className="mt-2 p-2 bg-red-100 border border-red-400 text-red-700 rounded">
                        {errors}
                    </div>
                ) : !errors && isValid ? (
                    <div className="mt-2 p-2 bg-green-100 border border-green-400 text-green-700 rounded">
                        The script is valid and ready to use.
                    </div>
                ) : null}
            </div>
            </>)}
        </div>
    );
}

export default SqlScriptEditorCard;