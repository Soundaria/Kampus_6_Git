import { Grid,Button,Modal,Card,Progress, Center } from "@mantine/core";
import { useDisclosure } from "@mantine/hooks";
import axios from "axios";
import { useState,useEffect } from "react";
import MyCourseCard from "../../Components/MyCourseCard";
import styles from './Coursecard.module.css';
import {jsPDF} from 'jspdf';
import autoTable  from "jspdf-autotable";
import ButtonField from "../../Components/Button";

export default function MyCourses(){
    const [opened, { open, close }] = useDisclosure(false);
    const[mycourse,setMyCourse]=useState([]);
    const[payment,setPayment]=useState([]);
    const[ispayment,setIsPayment]=useState(true);
    let imagelist=0;
    localStorage.removeItem("ImageList");
   // const colours = ['burlywood','bisque','cadteblue','darkcyan','darkgoldenrod','darkolivegreen','darkslategrey','lightblue','lavendar','lightgreen','tan','thistle'];
      
    const getMyCourse=()=>{
      axios.get(`https://localhost:7211/api/CandidateCourse/GetCourseOfCandidate/${JSON.parse(localStorage.getItem("CandidateId"))}`
      ).then((response)=>{
        setMyCourse(response.data);
    }).catch(err=>{alert(err.data);});
    }

    useEffect(()=>{ getMyCourse();},[]);

   function PDF(){
    const doc=new jsPDF();
    doc.setFont('inherit','italic');
    doc.setFontSize(20);
    doc.text("KampusLearn Bill Receipt",70,20);
    autoTable(doc,{
        margin: { top: 27 },
        head:[['S.No.','Candidate Name','Course','Trainer','Price','Time','Date','Payment']],
        body:[
            ['1',JSON.parse(localStorage.getItem("Name")),payment.courseName,payment.name,payment.price,payment.time,payment.weekday,"Payment done successfully"]
        ],
    })
    doc.save("Receipt.pdf");
   }

    const pay=(e)=>{
        //setDisable(true);
        axios.post(`https://localhost:7211/api/Payment/AddPayment/${JSON.parse(localStorage.getItem("CandidateId"))}`,{
            "courseId":e
        }).then((response)=>{
            setPayment(response.data);  
            if(Object.keys(response.data)==0){
                setIsPayment=false;
            } 
    }).catch(
        err=>{alert(err.data);
    }); 
    //PDF(); 
    }

    
    if(mycourse.length==0){
        return(
            <h2>No Courses added still..Reload to see courses if enrolled!!</h2>
        );
    }
    else{
    return(
        <>
        <Modal opened={opened} onClose={close}  >
          <Card withBorder shadow='lg'>
          <center> 
            {
               ispayment?<><p>Happy Learning..</p><ButtonField color="teal" onClick={PDF} type="submit">Download Receipt</ButtonField></>:<><p>No trainer is available right now..Will notify when available..</p><p>Happy Learning</p></>
            } 
          </center>
          </Card>  
        </Modal>
        <Grid  gutter="xl"  style={{paddingLeft:'10px',paddingTop:'25px',width:'100%'}}>
        { 
            mycourse.map((data)=>{
                return(
                    <Grid.Col span={3} className={styles.Card}>
                        <MyCourseCard  badge={data.status} duration={data.course.durationInHours}day={data.course.weekday}  time={data.course.time}    title={data.course.courseName}>
                         {  
                            data.status==="Active"?
                            <>
                            <br></br>
                            <Progress value={(data.course.progress*100)/(data.course.durationInHours)} label={`${(data.course.progress*100)/(data.course.durationInHours)}%`}  size={"xl"} radius={"xl"}  />
                            </>
                            :data.status==="Completed"?
                            <> 
                            <Center>
                            <b><p style={{color:'teal'}}>Course completed succesfully!!</p></b>
                            </Center>
                            {localStorage.setItem("ImageList",JSON.stringify(imagelist+=1))}
                            </>:
                            <Button variant="light" color="teal"  fullWidth mt="md" radius="md"  onClick={ e=> {open();pay(data.courseId);}} style={{fontSize:"15px"}}>
                             PAY
                            </Button>
                        }   
                        </MyCourseCard>
                    </Grid.Col>
                );
            })
        }
    </Grid>
    </>
    );
}
   }
