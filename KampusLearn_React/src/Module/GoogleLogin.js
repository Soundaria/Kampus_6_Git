// Import the functions you need from the SDKs you need
import { initializeApp } from "firebase/app";
// TODO: Add SDKs for Firebase products that you want to use
// https://firebase.google.com/docs/web/setup#available-libraries

// Your web app's Firebase configuration
const firebaseConfig = {
  apiKey: "AIzaSyDaN3GEfVxWfVqb6mbZwoegLYKaIDzZCZs",
  authDomain: "kampuslearn.firebaseapp.com",
  projectId: "kampuslearn",
  storageBucket: "kampuslearn.appspot.com",
  messagingSenderId: "958190771972",
  appId: "1:958190771972:web:f05532eb6d6a39d5d07a51"
};

// Initialize Firebase
export const GoogleLogin = initializeApp(firebaseConfig);