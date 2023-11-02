import Nav from "../../../Components/Nav";
import { Table } from '@mantine/core';
import { useState,useEffect } from "react";
import axios from 'axios';
import React from 'react';


export default function ViewCandidateCourse(){
    const [candidate,setCandidate]=useState([]);

    const getcourseandCandidate=()=>{
      axios.get('https://localhost:7211/api/CandidateCourse/GetCandidateAndCourse'
        ).then((response) => {
          setCandidate(response.data);
      }).catch(err=>{console.log(err.data)});
    }

    useEffect(() => {getcourseandCandidate();}, []);
    
    if(candidate.length===0){
      return(
        <Nav>
          <p>No Candidates are registered to the course</p>
        </Nav>
          
      );
    }
    else{
      const rows = candidate.map((element) => (
        <tr>
          <td>{element.candidate.name}</td>
          <td>{element.course.courseName}</td>
          <td>{element.status}</td>
          <td>{`${element.isPaymentDone}`}</td>
          <td>{new Date(element.createdAt).getFullYear()+"-"+new Date(element.createdAt).getMonth()+"-"+new Date(element.createdAt).getDate()}</td>
          {/* <td>{ <td>{new Date(element.updatedAt).getFullYear()+"-"+new Date(element.updatedAt).getMonth()+"-"+new Date(element.updatedAt).getDate()}</td>}</td> */}
        </tr>
      ));

      return(
        <>
                <Nav>
          <h3>List of Candidate and their courses are listed!!</h3>
          <hr/>
          <br/> <br/> 
          <Table horizontalSpacing="xs" striped >
          <thead>
            <tr>
              <th>Candidate Name</th>
              <th>Cousre Name</th>
              <th>Status</th>
              <th>Payment Done</th>
              <th>Created At</th>
              {/* <th>Updated At</th> */}
            </tr>
          </thead>
          <tbody>{rows}</tbody>
          </Table>
        </Nav>
        </>
     );
    }
}