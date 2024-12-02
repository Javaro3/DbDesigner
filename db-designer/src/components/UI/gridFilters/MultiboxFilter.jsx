import { useGridFilter } from 'ag-grid-react';
import React, { useCallback, useState } from 'react';
import MultiComboBox from '../inputs/MultiComboBox';
import Button from '../buttons/Button';

export default function MultiSelectFilter(props) {
    const [filter, setFilter] = useState([]);
    const [isDropdownOpen, setIsDropdownOpen] = useState(false);

    const doesFilterPass = useCallback(
        (params) => {
            return true;
        },
        [props?.model]
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
            <MultiComboBox
                size='small'
                options={props?.options || []}
                selected={filter}
                onChange={(selectedOptions) => setFilter(selectedOptions)}
                placeholder="Select values"
                onDropdownToggle={setIsDropdownOpen}
            />
            <Button
                size="small"
                className={`w-full ${isDropdownOpen ? 'mt-20' : 'mt-1'}`}
                onClick={() => props?.onModelChange(filter.length === 0 ? null : filter)}
            >
                Apply
            </Button>
        </div>
    );
};