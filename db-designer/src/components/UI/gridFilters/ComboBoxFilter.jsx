import React, { useCallback, useState } from 'react';
import ComboBox from '../inputs/ComboBox';
import Button from '../buttons/Button';
import { useGridFilter } from 'ag-grid-react';

export default function ComboBoxFilter(props) {
    const [filter, setFilter] = useState(null);
    const [isDropdownOpen, setIsDropdownOpen] = useState(false);

    const doesFilterPass = useCallback(
        (params) => {
            return true;
        },
        [filter, props?.columnField]
    );

    const afterGuiAttached = useCallback((params) => {
        if (params && !params.suppressFocus) {
        }
    }, []);

    useGridFilter({
        doesFilterPass,
        afterGuiAttached,
    });

    return (
        <div className="m-1 w-52">
            <div className="relative">
                <ComboBox
                    size='small'
                    options={props?.options}
                    selected={filter}
                    onChange={(selectedOption) => setFilter(selectedOption)}
                    placeholder="Select value"
                    onDropdownToggle={setIsDropdownOpen}
                />
            </div>
            
            <Button
                size="small"
                className={`w-full ${isDropdownOpen ? 'mt-20' : 'mt-1'}`}
                onClick={() => props?.onModelChange(filter == null ? null : filter)}
            >
                Apply
            </Button>
        </div>
    );
};