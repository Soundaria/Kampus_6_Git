import { Select } from '@mantine/core';
import { useState,useEffect } from 'react';

export default function SelectTime({value,onChange}) {
    
  return (
    
      // loading ? <p>loading</p> :
        
    <Select
    label="Time"
    placeholder="Select Time"
    searchable
    required
    nothingFound="No options"
    maxDropdownHeight={140}
    value={value}
    onChange={onChange}
    data={
        [
            { value: "Morning", label: "10:00AM-12:00PM" },
            { value: "Afternoon", label: "01:00PM-03:00PM" },
            { value: "Evening", label: "04:00PM-06:00PM" }
      ]
    }
    />
 
  );
  
}