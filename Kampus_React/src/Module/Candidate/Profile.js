import { Card, Grid, Center, CardSection ,Image} from "@mantine/core";
import axios from "axios";
import { useEffect,useState } from "react";
import { FaUserGraduate } from "react-icons/fa";
import { useNavigate } from "react-router-dom";
import ButtonField from "../../Components/Button";
import UpdateCandidate from "../../Components/Modal/Update Candidate";
import { BorderLeft } from "tabler-icons-react";


export default function Profile(){
  const navigate=useNavigate();
    const[candidate,setCandidate]=useState([]);
    const candidateId=JSON.parse(localStorage.getItem("CandidateId"));
    let imagelist=JSON.parse(localStorage.getItem('ImageList'));
    const for_loop = []
    for (let i=0;i<imagelist;i++) {
      for_loop.push(
        "https://banner2.cleanpng.com/20201012/veb/transparent-silver-badge-award-badge-5f841ec2b3fd26.2001069616024941467372.jpg"
      );
    };

    const getCandidate=()=>{
      axios.get(`https://localhost:7211/api/Candidate/GetCandidatebyId`,{
        headers:{
          'Authorization':`Bearer ${JSON.parse(localStorage.getItem('Candidatetoken'))}`
        }
      }).then((response) => {
        setCandidate(response.data);
      }).catch(err=>{console.log(err)});
    }

    useEffect(() => {getCandidate();}, []);

    // const signout=()=>{
    //     navigate('/');
    // }

    return(
            <>  
            <Grid style={{width:'100%',justifyContent:'center',paddingTop:'50px'}}> 
            <Grid.Col span={2}>
              <Card style={{marginTop:'8%',paddingLeft:'45px'}}>
              <FaUserGraduate size={150}/>
              </Card>
              <Grid>
              <UpdateCandidate/>
                {/* <ButtonField type='submit' color='red' onClick={signout}>Log Out</ButtonField> */}
              </Grid> 
            </Grid.Col> 
            <Grid.Col span={3}style={{fontSize:'20px'}} >
            <p><b>Name : </b>{candidate.name} </p> 
            <p><b>Address :</b> {candidate.address} </p>
            <p><b>Contact : </b> {candidate.contact} </p>
            <p><b>Email : </b> {candidate.email} </p>
            <br></br>
            <hr></hr>
            <div style={{BorderLeft:'10px '}} >
              <p><b>Badges!! </b></p>
              <Grid  gutter="xl"  style={{paddingLeft:'10px',paddingTop:'25px',width:'100%'}}>
              {
                for_loop.map((data,index)=>{
                  return(
                    <Grid.Col span={3} >
                    <Image style={{marginTop:'-30px'}}
                    src="https://banner2.cleanpng.com/20201012/veb/transparent-silver-badge-award-badge-5f841ec2b3fd26.2001069616024941467372.jpg"
                    alt="Badges"/>  
                    <br></br>
                    </Grid.Col>
                    
                ); 
                })
              }          
            
            </Grid>
            </div>
            </Grid.Col>
            </Grid>
            </>
        
       
    );
}