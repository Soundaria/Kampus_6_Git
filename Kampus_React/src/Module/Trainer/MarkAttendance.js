import ButtonField from "../../Components/Button";
import { useDisclosure} from '@mantine/hooks';
import { Modal, Group,Grid,Center,Card,Button } from '@mantine/core';
import { useState,useEffect } from "react";
import Registration from "../../Components/Registration";
import axios from "axios";

export default function Attendance(props){
    const [opened, { open, close }] = useDisclosure(false);
    const[start,setStart]=useState(new Date());
    const[attendance,setAttendance]=useState("");
    const[mycourse,setMycourse]=useState([]);
    const changeStart =(e)=>{
        setStart(e);
    }
    const trainerid=JSON.parse(localStorage.getItem("Trainer"));

    const MarkAttendance=(e)=>{
        axios.post(`https://localhost:7211/api/TrainerCourse/MarkAttendance/${e}/${trainerid.id.trainerId}`,{
            "attendance":start
        }
        ).then((response) => {
          setAttendance(response.data);
          //console.log(response.data);
          alert(response.data);
        }).catch(err=>{alert(err)});
    }
   
    
return(
    <>
    <Modal opened={opened} onClose={close} title="Attendance">
    <Center >
      <Card  withBorder radius="md"  style={{width:'100%',height:'23rem'}}>
      <Registration label='Date' value={start} onChange={changeStart}/>
      <br></br>
      <Center>
      <ButtonField color='teal' type='submit' onClick={()=>{MarkAttendance(props.courseId);close();}} >Mark Attendance</ButtonField> 
      </Center>
      <br></br><br></br>
      <p style={{color:'grey'}}>!!!Mark the attendance carefully...It can be done only once.It cannot be changed!!!</p>
      </Card>
    </Center>
    </Modal>
    <Group position="left" >
      <Button  color='teal' type='submit' variant="subtle" disabled={props.status==="Completed"?true:props.status==="Active"?false:""} onClick={()=>{open();}}> Attendance </Button> 
    </Group>
  </>
);
} 