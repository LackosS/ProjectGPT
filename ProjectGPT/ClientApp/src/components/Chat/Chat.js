import InputField from '../Chat/InputField'
import { Box } from '@mui/material';
import React, { useState, useEffect, useRef } from 'react';
import ChatItem from '.././Chat/ChatItem'

export default function Chat({currentConversation,setCurrentConversation,isLoaded}) {
    const chatContainerRef = useRef(null);

    function handleChange(question, answer) {
        const newArray = [ ...currentConversation ]
        newArray.push(answer)
        setCurrentConversation(newArray)
    }
    useEffect(() => {
        if (chatContainerRef.current) {
            chatContainerRef.current.scrollTop = chatContainerRef.current.scrollHeight;
        }
    }, [currentConversation])
    
    return (
        <Box ref={chatContainerRef} component='div' sx={{ marginTop: '10px', maxHeight: '1050px', overflowY:'auto' }}>
            { currentConversation && currentConversation.map((conversation, index) => {
                const [key, value] = Object.entries(conversation)
                if( !key || !value ) return
                return (
                    <Box key={index}>
                        <ChatItem sender={key[0]} text={key[1]} />
                        <ChatItem sender={value[0]} text={value[1]} />
                    </Box>
                )
            })}
            <InputField isLoaded={isLoaded} onChange={handleChange}/>
            </Box>
    )
}