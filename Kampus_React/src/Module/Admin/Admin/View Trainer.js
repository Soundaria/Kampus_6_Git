import Nav from "../../../Components/Nav";
import { Table,Grid } from '@mantine/core';
import { useEffect, useState } from "react";
import axios from 'axios';
import React from 'react';
import TrainerModal from "../../../Components/Modal/Add Trainer Modal";
import DeleteModal from "../../../Components/Modal/Delete Modal";

export default function ViewTrainerofAdmin(){
    const[trainer,setTrainer]=useState([]);
   
     const getTrainer=()=>{
      let token=JSON.parse(localStorage.getItem('Authtoken'));
      axios.get('https://localhost:7211/api/Admin/GetTrainerbyAdmin',{
       headers:{
         'Authorization':`Bearer ${token}`,
        }
      }).then((response)=>{
         setTrainer(response.data);
      }).catch(err=>{alert(err.data);});
  }

    useEffect(()=>{ getTrainer();},[]);

    if(trainer.length===0){
      return(
        <Nav>
          <p>No Trainers are added!!</p>
        </Nav>
      );
    }
    else{
    const rows = trainer.map((element) => (

        <tr key={element.trainerId}>
          <td>{element.trainerId}</td>
          <td>{element.name}</td>
          <td>{element.contact}</td>
          <td>{element.email}</td>
          <td>{element.address}</td>
          <td>{element.qualification}</td>
          <td>{element.yearOfExperience}</td>
          <td>{JSON.parse(localStorage.getItem("Name"))}</td>
          <td>{new Date(element.createdAt).getFullYear()+"-"+new Date(element.createdAt).getMonth()+"-"+new Date(element.createdAt).getDate()}</td>
          {/* <td>{ <td>{new Date(element.updatedAt).getFullYear()+"-"+new Date(element.updatedAt).getMonth()+"-"+new Date(element.updatedAt).getDate()}</td>}</td> */}
        </tr>
      ));

        return(
            <Nav>
            <h3>List of trainer by Admin is listed!!</h3>
            <hr/>
            <br/> <br/> 
            <Table horizontalSpacing="xs" striped >
            <thead>
              <tr>
                <th>Trainer Id</th>
                <th>Name</th>
                <th>Contact</th>
                <th>Email</th>
                <th>Address</th>
                <th>Qualification</th>
                <th>Years of Experience</th>
                <th>Admin  Name</th>
                <th>Created At</th>
                {/* <th>Updated At</th> */}
              </tr>
            </thead>
            <tbody>{rows}</tbody>
            </Table>
            
          </Nav>  
        );
    }
}