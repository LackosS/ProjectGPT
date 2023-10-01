import React, {useState} from 'react';
import { Route, Routes } from 'react-router-dom';
import AppRoutes from './AppRoutes';
import NavMenu from "./components/NavMenu/NavMenu";
import './global.css';
import Chat from './components/Chat/Chat'
import { Grid } from '@mui/material';
import { loadConversation } from '../src/API/api';

export default function App() {
    const [currentConversation, setCurrentConversation] = useState([])
    const [isLoaded, setLoaded] = useState(true)
   const appStyle = {
       backgroundColor: 'black',
       minHeight: '90vh',
       color: 'white',
       overflow: 'hidden'
    };

    const loadChat = (id) => {
        setLoaded(true);
        loadConversation(id).then((conversation) => {
            setCurrentConversation(conversation);
        });
    };

    const clearConversation = () => {
        setCurrentConversation([]);
    }

    return (
        <Grid container sx={appStyle}>
            <Grid item xs={12} sm={4} md={2}><NavMenu setLoaded={setLoaded} loadChat={loadChat} clearConversation={clearConversation}/></Grid>
            <Grid item xs={12} sm={8} md={10} sx={{ position: 'relative' }}><Chat isLoaded={isLoaded} currentConversation={currentConversation} setCurrentConversation={setCurrentConversation} /></Grid>
            {/*<Routes>
                {AppRoutes.map((route, index) => {
                    const { element, ...rest } = route;
                    return <Route key={index} {...rest} element={element} />;
                })}
            </Routes>*/}
       </Grid>
   );
}
