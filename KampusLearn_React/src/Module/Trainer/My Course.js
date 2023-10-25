import { useState,useEffect } from "react";
import NavTrainer from "../../Components/NavTrainer";
import axios from "axios";
import { Table } from "@mantine/core";
import View from "./ViewParticipants";
import Attendance from "./MarkAttendance";
import ButtonField from "../../Components/Button";
import { useNavigate } from "react-router-dom";
import { AiOutlineEye } from "react-icons/ai";



export default function MyCourse(){
    const[mycourse,setMycourse]=useState([]);
    const[participants,setParticipants]=useState([]);
    const trainerid=JSON.parse(localStorage.getItem("Trainer"));
    const navigate=useNavigate();
    
    const getmycourse=()=>{
        axios.get(`https://localhost:7211/api/TrainerCourse/GetCourseOfTrainer/${trainerid.id.trainerId}`
        ).then((response) => {
          setMycourse(response.data);
          console.log(response.data);
        }).catch(err=>{console.log(err)});
      }
  
      useEffect(() => {getmycourse();}, []);

      
      if(mycourse.length===0){
        return(
            <NavTrainer>
            <p>No Courses were added still!!</p>
          </NavTrainer> 
        );
        
      }
      else{
        const rows=mycourse.map((element)=>(
          <tr key={element.id}>
            <td>{element.course.courseName}</td>
            <td>{element.course.durationInHours}</td>
            <td>{element.course.time}</td>
            <td>{element.course.weekday}</td>
            <td><ButtonField  color='teal' type='submit' onClick={()=>{ navigate(`/viewparticipants/${element.course.courseId}`) }} >View</ButtonField></td>
            <td><Attendance courseId={element.course.courseId} status={element.status} ></Attendance></td>
            <td>{(element.course.progress*100)/(element.course.durationInHours)}</td>
          </tr>
  
      ));
        return(
            <NavTrainer>
            <h3>List of courses are listed!!</h3>    
        <hr/>
        <br/> <br/>
        <Table horizontalSpacing="xs" striped >
        <thead>
          <tr>
            <th>Course Name</th>
            <th>Duration</th>
            <th>Time</th>
            <th>Day</th>
            <th>Participants</th>
            <th>Mark Attendance</th>
            <th>Progress</th>
          </tr>
        </thead>
        <tbody>{rows}</tbody>
        </Table>
        <br/><br/>
        <hr/>
            </NavTrainer>
        );
      }
}