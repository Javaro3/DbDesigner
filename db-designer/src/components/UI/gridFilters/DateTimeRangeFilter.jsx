import React, { useCallback, useState } from 'react';
import DateTimePicker from '../inputs/DateTimePicker';
import Button from '../buttons/Button';
import { useGridFilter } from 'ag-grid-react';

export default function DateTimeRangeFilter(props) {
    const [fromTime, setFromTime] = useState('');
    const [toTime, setToTime] = useState('');

    const doesFilterPass = useCallback(
        (params) => {
            return true;
        },
        [fromTime, toTime, props.columnField]
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
            <DateTimePicker
                label="From"
                selectedDateTime={fromTime}
                onChange={setFromTime}
                placeholder="Start date and time"
                className="mb-2"
            />
            <DateTimePicker
                label="To"
                selectedDateTime={toTime}
                onChange={setToTime}
                placeholder="End date and time"
                className="mb-2"
            />
            
            <Button
                size="small"
                className="w-full mt-2"
                onClick={() => props.onModelChange((!fromTime && !toTime) ? null : { fromTime, toTime })}
            >
                Apply
            </Button>
        </div>
    );
};