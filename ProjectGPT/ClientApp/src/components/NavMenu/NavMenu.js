import React, { useState, useEffect } from 'react';
import { Box, Typography, Button, ListItem, List, Avatar} from '@mui/material';
import logoImage from '../../pictures/gpt_logo.png';
import { NavStyle } from '../NavMenu/NavStyle'
import ChatDialog from '../ChatDialog/ChatDialog'
import { getConversations, deleteConversation} from '../../API/api'
import DeleteOutlineIcon from '@mui/icons-material/DeleteOutline';

export default function NavMenu({ loadChat, clearConversation, setLoaded}) {
    const [showModal, setShowModal] = useState(false);
    const [Conversations, setConversations] = useState([]);

    const openModal = () => {
        setShowModal(true);
    };

    const fetchConversations = () => {
        getConversations().then(res => {
            setConversations(res);
        })
    }
    const deleteConversations = () => {
        const deletePromises = Conversations.map((conversation) => {
            return deleteConversation(conversation.id);
        });
        Promise.all(deletePromises)
            .then(() => {
                fetchConversations();
            })
    };
    useEffect(() => {
        fetchConversations()
    }, [])

    return (
        <Box sx={NavStyle.nav}>
            <Box sx={NavStyle.logoContainer}>
                <Avatar src={logoImage} alt="Logo" sx={NavStyle.logo} />
                <Typography sx={NavStyle.logoText}>ProjectGPT</Typography>
            </Box>
            <Box sx={NavStyle.newChat}>
                <Button variant="outlined" sx={NavStyle.button} onClick={openModal}>
                    <span style={NavStyle.buttonText}>
                        New chat
                    </span>
                </Button>
                <Button variant="outlined" color="error" sx={NavStyle.button} onClick={deleteConversations }startIcon={<DeleteOutlineIcon />}>
                    Delete
                </Button>
                <ChatDialog setLoaded={setLoaded} showModal={showModal} setShowModal={setShowModal} onChatCreate={fetchConversations} clearConversation={clearConversation} />
            </Box>
            <hr style={NavStyle.hr} />
            <List sx={NavStyle.list}>
                {Conversations && Conversations.map((conversation, index) => {
                    return (
                        <ListItem key={index}>
                            <Button onClick={()=>loadChat(conversation.id) }variant="outlined" sx={NavStyle.button}>
                                <span style={NavStyle.buttonText}>
                                    { conversation.name}
                                </span>
                            </Button>
                        </ListItem>
                      )
                }) }
            </List>
        </Box>
    );
}