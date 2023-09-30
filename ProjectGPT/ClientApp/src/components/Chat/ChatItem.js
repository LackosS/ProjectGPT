import {  Box,Typography } from '@mui/material';
export default function ChatItem({ sender, text }) {
    const isSenderUser = sender === 'user';
    return (
        <Box sx={{ marginLeft: '10px' }}component='div'>
            <Typography variant="subtitle1" sx={{ color: 'white' }}><span style={{ color: isSenderUser ? 'green' : '#2196F3' }}>{sender}</span> : {text}</Typography>
            </Box>
        )
}