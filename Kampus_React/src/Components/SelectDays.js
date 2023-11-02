import { Select } from '@mantine/core';
import { useState,useEffect } from 'react';
import axios from 'axios';

export default function SelectDays({value,onChange}) {
 
  return (
    
    <Select
    label="Days"
    placeholder="Select Days"
    searchable
    required
    nothingFound="No options"
    maxDropdownHeight={140}
    value={value}
    onChange={onChange}
    data={
        [
            { value: "Mon-Wed", label: "Mon-Wed" },
            { value: "Thurs-Sat", label: "Thurs-Sat" },
            { value: "Mon,Wed,Fri", label: "Mon,Wed,Fri" },
            { value: "Tue,Thurs,Sat", label: "Tue,Thurs,Sat" }
      ]
    }
    />

  );
  
}