import {  Grid,Blockquote,Header,Text } from "@mantine/core";
import SignupCard from "../Components/LandingCard";
import styles from './LandingPage.module.css';

function Landing(){
return(
    <>
        <Header height={50} p="xs" style={{backgroundColor:'teal',display:'flex',alignItems:'center'}}>
        <Text component="a" href="/" style={{color:'whitesmoke',fontFamily:'inherit',fontWeight:'bolder',fontSize:'xx-large',textAlign:'left',textIndent:'50px',fontStyle:'italic'}}>CoursePedia</Text>
        </Header> 

        <Blockquote cite="– Confucius" color='dark' style={{paddingTop:'5%',paddingLeft:'10%',fontSize:'30px',fontStyle:'italic',color:'black',fontWeight:'bold',fontFamily:'serif'}} >
          He who learns but does not think, is lost! He who thinks but does not learn is in great danger.
        </Blockquote>
        
        <Grid justify='space-around' style={{paddingTop:'5%',width:'100%'}}>
            <Grid.Col span={3} className={styles.Card}> 
                <SignupCard title="Admin" text="Log In to the rule KampusLearn"  />
            </Grid.Col>

            <Grid.Col span={3} className={styles.Card}> 
                <SignupCard title="Trainer" text="Log In and train Young Minds" />
            </Grid.Col>

            <Grid.Col span={3} className={styles.Card}> 
                <SignupCard title="User" text="Register or Log In to be the next Ruler in this devolping World" />
            </Grid.Col> 
        </Grid>
    </>

);
}

export default Landing;