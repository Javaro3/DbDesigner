import React from 'react';
import { Autocomplete, TextField } from '@mui/material';

const BooleanFilter = React.memo((props) => {
  const { item, applyValue } = props;

  const options = [
    { label: 'Yes', value: true },
    { label: 'No', value: false },
  ];

  const handleFilterChange = (event, value) => {
    applyValue({ ...item, value: value.map((v) => v.value) });
  };

  return (
    <Autocomplete
      multiple
      options={options}
      getOptionLabel={(option) => option.label} 
      value={options.filter((option) => item.value?.includes(option.value))}
      onChange={handleFilterChange}
      renderInput={(params) => (
        <TextField
          {...params}
          variant="outlined"
          placeholder="Select values"
          size="small"
          fullWidth
        />
      )}
      style={{ width: 300 }}
    />
  );
});

export default BooleanFilter;