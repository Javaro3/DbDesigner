import React, { useCallback, useEffect, useState } from 'react';
import { Autocomplete, TextField } from '@mui/material';
import { getForCombobox } from '../../../utils/apiHelper';

const EntityFilter = React.memo((props) => {
  const { item, applyValue } = props;
  const [entities, setEntities] = useState([]);

  const fetchEntities = useCallback(async () => {
    try {
      const result = await getForCombobox(item.operator);
      setEntities(result);
    } catch (error) {
      console.error(`Failed to fetch ${item.operator}:`, error);
    }
  }, [item.operator]);

  useEffect(() => {
    fetchEntities();
  }, [fetchEntities]);

  const handleFilterChange = (event, value) => {
    applyValue({ ...item, value: value.map((entity) => entity.id) });
  };

  return (
    <Autocomplete
      multiple
      options={entities}
      getOptionLabel={(option) => option.name}
      value={entities.filter((entity) => item.value?.includes(entity.id))}
      onChange={handleFilterChange}
      renderInput={(params) => (
        <TextField
          {...params}
          variant="outlined"
          placeholder={`Select ${item.operator.toLowerCase()}s`}
          size="small"
          fullWidth
        />
      )}
      style={{ width: 300 }}
    />
  );
});

export default EntityFilter;