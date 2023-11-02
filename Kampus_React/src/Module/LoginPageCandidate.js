import ButtonField from "../Components/Button";
import TextBox from "../Components/TextBox";
import { Center,Grid,Card, PasswordInput,Header,Text } from "@mantine/core";
import {  useState,useEffect } from "react";
import axios from 'axios';
import { useNavigate } from "react-router-dom";
import { GoogleLogin } from '@react-oauth/google';

function LoginCandidate(){
    const navigate=useNavigate();
    const [email,setEmail]=useState("");
    const [password,setPassword]=useState("");
    const emailfunc=(e)=>{
        setEmail(e.target.value);
    }
    const passfunc=(e)=>{
        setPassword(e.target.value);
    }
    //Google login function
    const responseMessage = (response) => {
        console.log(response);
    };
    const errorMessage = (error) => {
        console.log(error);
    };
    const Validation=(e)=>{
        e.preventDefault();
        
        axios.post('https://localhost:7211/api/Candidate/ValidatingCandidate',{
            email,
            password
        }).then((response)=>{
           
            console.log(response.data);
            localStorage.setItem("Candidatetoken",JSON.stringify(response.data.jwt));
            localStorage.setItem("Name",JSON.stringify(response.data.id.name));
            localStorage.setItem("CandidateId",JSON.stringify(response.data.id.candidateId));
            navigate('/candidate');
        }).catch(err=>{
            alert(err.response.data);
        });

    }

    return(
        <>
         <Header height={50} p="xs" style={{backgroundColor:'teal',display:'flex',alignItems:'center'}}>
        <Text component="a" href="/" style={{color:'whitesmoke',fontFamily:'inherit',fontWeight:'bolder',fontSize:'xx-large',textAlign:'left',textIndent:'50px',fontStyle:'italic'}}>CoursePedia</Text>
        </Header>
          <h2 style={{textAlign:'center',paddingTop:'5%'}}>Welcome Candidate!!</h2>
        <Center>
            <Card withBorder radius="md"  style={{width:'28%',marginTop:'3%',boxShadow:'0 0 11px rgba(33,33,33,.2)'}}>
            <form  onSubmit={Validation}> 
                <Grid justify="center" align="center">
                <Grid.Col span={10}>
                    <TextBox type="text" label="Email" placeholder="Enter your Email Address" value={email} onChange={emailfunc}  required={true}/>
                </Grid.Col>
                <br/>
                <Grid.Col span={10}>
                    <PasswordInput  label="Password" placeholder="Enter your password" value={password} onChange={passfunc}   required={true}/>
                </Grid.Col>
                <br/>
                </Grid>
                <br/>
                <Center>
                    <ButtonField type="submit" color='teal'>Login</ButtonField>
                </Center>
            </form>
            </Card>
           
        </Center>
        </>
    );
}

export default LoginCandidate;