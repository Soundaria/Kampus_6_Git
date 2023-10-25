import { useState } from 'react';
import { DateInput } from '@mantine/dates'

export default function Registration({label,value,onChange}) {
  //const [value, setValue] = useState("");
  return (
    <DateInput
      value={value}
      onChange= {(e) => {onChange(e)}}
      required
      label={label}
      placeholder="Select date"
      maw={400}
      mx="auto"
    />
  );
}