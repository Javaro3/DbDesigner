import { useGridFilter } from 'ag-grid-react';
import React, { useCallback, useRef, useState } from 'react';
import Input from '../inputs/Input';
import Button from '../buttons/Button';


export default function TextFilter({ model, onModelChange, getValue }) {
    const refInput = useRef(null);
    const [filter, setFilter] = useState('');

    const doesFilterPass = useCallback(
        (params) => {
            return true;
        },
        [model]
    );

    const afterGuiAttached = useCallback((params) => {
        if (!params || !params.suppressFocus) {
            refInput.current.focus();
        }
    }, []);

    useGridFilter({
        doesFilterPass,
        afterGuiAttached,
    });

    return (
        <div className="m-1">
            <div>
                <Input
                    size='small'
                    refInput={refInput}
                    type="text"
                    value={filter}
                    onChange={({ target: { value } }) => setFilter(value)}
                    placeholder="Input text"
                />
                <Button size='small' className='mt-1 w-full' onClick={() => onModelChange(filter === '' ? null : filter)}>Apply</Button>
            </div>
        </div>
    );
};