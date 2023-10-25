
import { Card,  Text, Badge, Group,Image,Center,Container} from '@mantine/core';




function CourseCard(props,{style}) {
  return (
    <>
    
      {/* <Modal opened={opened} onClose={close}  >
        
          <Card withBorder shadow='lg'>
          <center>
          <p>Course Enrolled Successfully!!</p>
          <p>Do the payment for the activation of course..</p>
          <ButtonField color='teal' >Pay</ButtonField>
          <ButtonField color='red' onClick={close}>Later</ButtonField>
          </center>
          </Card>
         
      </Modal> */}

    <Card shadow="sm" padding="lg" radius="md"  withBorder >
      <Card.Section >
        {/* <Container style={{height:'160px',backgroundColor:'bisque'}}>
          <Text weight={500} size={20} style={{fontFamily:'cursive',textAlign:'center',paddingTop:'20%'}}>{props.title}</Text>
        </Container>  */}
         <Image
          src="https://every-tuesday.com/wp-content/uploads/2016/04/courses.jpg"
          height={200}
          alt="Courses"
        />          
      </Card.Section >
      <Group position="apart" mt="md" mb="xs">
      <Text weight={500} size={20} style={{fontFamily:'cursive'}}>{props.title}</Text>
      <Badge color="cyan" variant='light'>{props.badge}</Badge>
      </Group>
      <Group position="left" >
        <Badge color="cyan" variant='light'>Rs.{props.price}</Badge>
        <Badge color="cyan" variant='light'>{props.duration} Hours</Badge>
        <Badge color="cyan" variant='light'>{props.time}</Badge>
        <Badge color="cyan" variant='light'>{props.day}</Badge>
        <Badge color="cyan" variant='light'>{props.startdate}</Badge>
        {/* <Badge color="cyan" variant='light'>{props.enddate}</Badge> */}
      </Group>
      {props.children}
    {/* <Button variant="light" color="teal" fullWidth mt="md" radius="md" onClick={e=>{open();enroll();}} style={{fontSize:"20px"}}>
        Enroll
      </Button> */}
    </Card>
    </>
  );
}

export default CourseCard;