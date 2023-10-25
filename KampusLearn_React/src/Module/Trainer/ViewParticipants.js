
import { Modal, Group,Grid,Center,Card,Table } from '@mantine/core';
import axios from "axios";
import { useState,useEffect } from "react";
import NavTrainer from '../../Components/NavTrainer';
import { useParams } from 'react-router-dom';


export default function View(){
    const[participants,setParticipants]=useState([]);
    //const trainerid=JSON.parse(localStorage.getItem("Trainer"));
     
      const viewparticipants=(courseId)=>{
        axios.get(`https://localhost:7211/api/TrainerCourse/GetParticipants/${courseId}`
        ).then((response) => {
          setParticipants(response.data);
          //console.log(response.data);
        }).catch(err=>{console.log(err)});
      
      }  
      let {id}=useParams();
      const QueryParams=()=>{
          viewparticipants(id);
      }
      useEffect(()=>{ QueryParams();},[]);


    const rows=participants.map((element)=>(
        <tr >
          <td>{element.candidate.name}</td>
          <td>{element.course.courseName}</td>
          <td>{element.course.time}</td>
          <td>{element.course.weekday}</td>
        </tr>
    ));

    return (
        <>
         <NavTrainer>
          <h3>List of participants are listed!!</h3>    
        <hr/>
        <br/> <br/>
        <Table horizontalSpacing="xs" striped >
        <thead>
          <tr>
            <th>Candidate Name</th>
            <th>Course Name</th>
            <th>Time</th>
            <th>Day</th>
          </tr>
        </thead>
        <tbody>{rows}</tbody>
        </Table>
        </NavTrainer>
        </>
 );

}
