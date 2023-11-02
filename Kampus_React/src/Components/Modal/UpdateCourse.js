import { useDisclosure} from '@mantine/hooks';
import { Modal, Group,Grid,Center,Card } from '@mantine/core';
import ButtonField from '../Button';
import TextBox from '../TextBox';
import { useState,useEffect } from 'react';
import axios from 'axios';
import Registartion from '../Registration';
import Registration from '../Registration';

export default function  UpdateCourse(props){
 
  const [opened, { open, close }] = useDisclosure(false);
  const [name,setName]=useState(""); 
  const [category,setCategory]=useState("");
  const [price,setPrice]=useState("");
  const [duration,setDuration]=useState("");
  const [time,setTime]=useState("");
  const [day,setDay]=useState("");
  const [start,setStart]=useState(new Date());
  const [end,setEnd]=useState(new Date());


  const onClick= ()=>{
    axios.get(`https://localhost:7211/api/Course/GetCoursebyid/${props.id}`
    ).then((response)=>{
      setName(response.data.courseName);
      setCategory(response.data.courseCategory);
      setPrice(response.data.price);
      setDuration(response.data.durationInHours);
      setTime(response.data.time);
      setDay(response.data.weekday);
   }).catch(err=>{
     alert(err.data);
   })
  }
  

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
  const changeTime=(e)=>{
    setTime(e.target.value);
  } 
  const changeDay=(e)=>{
    setDay(e.target.value);
  } 
  const changeStart=(e)=>{
    setStart(e);
  }
  const changeEnd=(e)=>{
    setEnd(e);
  }
  const UpdateCourse= ()=>{
     
    axios.put(`https://localhost:7211/api/Admin/UpdateCourse/${props.id}`,{
        "courseName":name,
        "courseCategory":category,
        "price":price,
        "durationInHours":duration,
        "time":time,
        "weekday":day,
        "startdate":start,
        "enddate":end
    },{
      headers:{
        'Authorization':`Bearer ${JSON.parse(localStorage.getItem('Authtoken'))}`
      }
  }).then((response)=>{
        alert(response.data);
    }).catch(err=>{
      alert(err.data);
    })
   
  }
  return (
    <>
      <Modal opened={opened} onClose={close} title="Update Course">
      <Center >
        <Card  withBorder radius="md"  style={{width:'100%'}}>
       <form onSubmit={UpdateCourse}>
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
                <TextBox type="text" label="Price" placeholder="Enter the Price" value={price}  onChange={changePrice} required={true}/>
            </Grid.Col>
            <br/>
            <Grid.Col span={10}>
                <TextBox type="text" label="Duration of Course" placeholder="Enter the Duration" value={duration} onChange={changeDuration} required={true}/>
            </Grid.Col>
            <br/>
            <Grid.Col span={10}>
                <TextBox type="text" label="Time" placeholder="Enter Time" value={time} onChange={changeTime} required={true}/>
            </Grid.Col>
            <br/>
            <Grid.Col span={10}>
                <TextBox type="text" label="Days" placeholder="Enter the Day" value={day} onChange={changeDay} required={true}/>
            </Grid.Col>
            <br/>
            <Grid.Col span={10}>
                <Registration label='Start date' value={start} onChange={changeStart}/>
            </Grid.Col>
            <br/>
            <Grid.Col span={10}>
                <Registration label='End date' value={end} onChange={changeEnd}/>
            </Grid.Col>
             </Grid>
            <br/>
            <Center>
                <ButtonField type="submit" color='teal' >Update Course</ButtonField>
            </Center>
       </form>
       
    </Card>
    </Center>
    
      </Modal>

      <Group position="left" >
      <ButtonField  color='teal' type='submit' onClick={()=>{ onClick(props.id);open();}} >Update Course</ButtonField>
      </Group>
    </>
  );
}
