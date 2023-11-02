import { useDisclosure} from '@mantine/hooks';
import { Modal, Group,Grid,Center,Card } from '@mantine/core';
import ButtonField from '../Button';
import TextBox from '../TextBox';
import { useState,useEffect } from 'react';
import axios from 'axios';
import SelectTime from '../SelectTime';
import SelectDays from '../SelectDays';
import Registration from '../Registration';

import DatePicker from "react-datepicker";


export default function CourseModal() {
  const [opened, { open, close }] = useDisclosure(false);
  const [admin,setAdmin]=useState(JSON.parse(localStorage.getItem("Admin")));
  const [name,setName]=useState("");
  const [category,setCategory]=useState("");
  const [price,setPrice]=useState("");
  const [duration,setDuration]=useState("");
  const [day,setDay]=useState("");
  const [time,setTime]=useState("");
  const [start,setStart]=useState(new Date());
  const [end,setEnd]=useState(new Date());

  const changeName=(e)=>{
    setName(e.target.value);
  }
  const changeCategory=(e)=>{
    setCategory(e.target.value);
  }
  const changePrice=(e)=>{
    setPrice(e.target.value);
  } 
  const changeDuration=(e)=>{
    setDuration(e.target.value);
  }
  const changeDays=(e)=>{
    setDay(e);
  }
  const changeTime=(e)=>{
    setTime(e);
  }
  const changeStart=(e)=>{
    setStart(e);
  }
  const changeEnd=(e)=>{
    setEnd(e);
  }
  
  const AddCourse= ()=>{
   // console.log(start);
    axios.post(`https://localhost:7211/api/Admin/AddCourse`,{
      "courseName":name,
      "courseCategory":category,
      "price":price,
      "durationInHours":duration,
      "weekday":day,
      "time":time,
      "startdate":start,
      "enddate":end
    },{
      headers:{
        'Authorization':`Bearer ${JSON.parse(localStorage.getItem('Authtoken'))}`
      }
  }).then((response)=>{
        alert(response.data);
        console.log(response.data);
    }).catch(err=>{
      alert(err.data);
    })
  }
  return (
    <>
      <Modal opened={opened} onClose={close} title="Add Course">
      <Center >
        <Card  withBorder radius="md"  style={{width:'100%'}}>
        <form onSubmit={AddCourse}>
        <Grid justify="center" align="center">
            <Grid.Col span={10}>
                <TextBox type="text" label="Course Name" placeholder="Enter the Course Name" value={name} onChange={changeName} required={true}/>
            </Grid.Col>
            <br/>
            <Grid.Col span={10}>
                <TextBox type="text" label="Course Category" placeholder="Enter the Course Category" value={category} onChange={changeCategory} required={true}/>
            </Grid.Col>
            <br/>
            <Grid.Col span={10}>
                <TextBox type="text" label="Price" placeholder="Enter the Price" value={price} onChange={changePrice} required={true}/>
            </Grid.Col>
            <br/>
            <Grid.Col span={10}>
                <TextBox type="text" label="Duration of Course" placeholder="Enter the Duration" value={duration} onChange={changeDuration} required={true}/>
            </Grid.Col>
            <br/>
            <Grid.Col span={10}>
                <SelectTime value={time} onChange={changeTime} />
            </Grid.Col>
            <br/>
            <Grid.Col span={10}>
               <SelectDays value={day} onChange={changeDays}/>
            </Grid.Col>
            <br/>
            <Grid.Col span={10}>
                <Registration label='Start date' value={start} onChange={changeStart}/>
                {/* <DatePicker selected={start} onChange={(date) => setStart(date)} /> */}
            </Grid.Col>
            <br/>
            <Grid.Col span={10}>
                <Registration label='End date' value={end} onChange={changeEnd}/>
                {/* <DatePicker label="End date" selected={end} onChange={(date) => setEnd(date)} /> */}
            </Grid.Col>
             </Grid>
            <br/>
            <Center>
                <ButtonField type="submit" color='teal' >Add Course</ButtonField>
            </Center>
       </form>
       
    </Card>
    </Center>
    
      </Modal>

      <Group position="right" >
      <ButtonField  color='teal' type='submit' onClick={open} >Add Course</ButtonField>
      </Group>
    </>
  );
}