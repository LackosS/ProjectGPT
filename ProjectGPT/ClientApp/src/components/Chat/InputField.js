import React, { useState } from 'react';
import { TextField, Box, Button } from '@mui/material';
import { getAnswer } from '../../API/api'
import { inputStyle } from '../styles/inputStyle'

export default function InputField({onChange,isLoaded}) {
    const [question, setQuestion] = useState("");
    const style = {
        position: 'absolute',
        left: '0',
        bottom: '0',
        width: '100%',
        display: 'flex',
        marginLeft: '10px',
    }
    function sendQuestion() {
        getAnswer(question).then(answer => {
            onChange(question, answer);
            setQuestion("");
        })
    }
    function handleQuestionChange(event) {
        setQuestion(event.target.value);
    }
    return (
        <Box sx={style}>
            <TextField value={question} disabled={isLoaded }sx={inputStyle} id="outlined-basic" label="Text" variant="outlined" onChange={handleQuestionChange}/>
            <Button disabled={isLoaded} variant="outlined" onClick={sendQuestion}>Send</Button>
         </Box>
        )
}