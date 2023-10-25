import { Center, Card,Grid,Header,Text } from "@mantine/core";
import { useNavigate } from "react-router-dom";
import ButtonField from "../Components/Button";
import TextBox from "../Components/TextBox";
import axios from "axios";
import { useState } from "react";


function Signup(){

const navigate=useNavigate();

const [name,setName]=useState("");
const [email,setEmail]=useState("");
const [password,setPassword]=useState("");
const [contact,setContact]=useState("");
const [address,setAddress]=useState("");

const changeName=(e)=>{
    setName(e.target.value);
}
const changeEmail=(e)=>{
  setEmail(e.target.value);
}
const changePassword=(e)=>{
  setPassword(e.target.value);
}
const changeContact=(e)=>{
  setContact(e.target.value);
}
const changeAddress=(e)=>{
  setAddress(e.target.value);
}

const func=()=>{
    axios.post('https://localhost:7211/api/Candidate/AddCandidate',{
            name,
            email,
            password,
            address,
            contact
        }).then((response)=>{
            navigate('/logincandidate');
            alert(response.data);
            
        }).catch(err=>{
            alert(err.response.data);
            console.log(err);
        });
   
}
return(
    <>
        <Header height={50} p="xs" style={{backgroundColor:'teal',display:'flex',alignItems:'center'}}>
        <Text component="a" href="/" style={{color:'whitesmoke',fontFamily:'inherit',fontWeight:'bolder',fontSize:'xx-large',textAlign:'left',textIndent:'50px',fontStyle:'italic'}}>CoursePedia</Text>
        </Header> 
    
    <Center >
        <Card  withBorder radius="md"  style={{width:'28%',marginTop:'5%',boxShadow:'0 0 11px rgba(33,33,33,.2)'}}>
       <form onSubmit={func}>
       <Grid justify="center" align="center">
            <Grid.Col span={10}>
                <TextBox type="text" label="Name" placeholder="Enter your Name" value={name} onChange={changeName} required={true}/>
            </Grid.Col>
            <br/>
            <Grid.Col span={10}>
                <TextBox type="text" label="Email" placeholder="Enter your Email Address" value={email} onChange={changeEmail} required={true}/>
            </Grid.Col>
            <br/>
            <Grid.Col span={10}>
                <TextBox type="password" label="Password" placeholder="Enter a strong password" value={password} onChange={changePassword} required={true}/>
            </Grid.Col>
            <br/>
            <Grid.Col span={10}>
                <TextBox type="text" label="Contact" placeholder="Enter your Contact Number" value={contact} onChange={changeContact} required={true}/>
            </Grid.Col>
            <br/>
            <Grid.Col span={10}>
                <TextBox type="text" label="Address" placeholder="Enter your Address" value={address} onChange={changeAddress} required={true}/>
            </Grid.Col>
             </Grid>
            <br/>
            <Center>
                <ButtonField  color='teal' type='submit' >Sign Up</ButtonField>
                <a href='/logincandidate' style={{color:'black'}}>Already Have an account? Login</a>
            </Center>
       </form>
       
    </Card>
    </Center>
    </>
);
}

export default Signup;