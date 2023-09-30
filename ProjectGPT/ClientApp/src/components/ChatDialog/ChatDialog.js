import { Box, Typography, Button,Stepper, Step, StepLabel, TextField, Switch, FormControlLabel, Dialog, DialogTitle, DialogContent, DialogActions } from '@mui/material';
import React, { useState } from 'react';
import { DialogStyle } from '../ChatDialog/DialogStyle'
import { CreateChat, CreateChatWithBehavior } from '../../API/api'
import { inputStyle } from '../styles/inputStyle'

export default function ChatDialog({ setLoaded, showModal, setShowModal, onChatCreate, clearConversation}) {
    
    const [activeStep, setActiveStep] = useState(0);
    const [version, setVersion] = useState('3.5');
    const [behavior, setBehavior] = useState('');

    const steps = ['Set Version', 'Define Behavior'];

    const handleNext = () => {
        setActiveStep((prevActiveStep) => prevActiveStep + 1);
    };

    const handleBack = () => {
        setActiveStep((prevActiveStep) => prevActiveStep - 1);
    };

    const toggleStep = () => {
        setVersion((prevVersion) => prevVersion === '3.5' ? '4.0' : '3.5');
    };

    const createChat = () => {
        const handleChatCreation = () => {
            onChatCreate()
            clearConversation()
            setLoaded(false)
            setVersion('3.5');
            setBehavior('');
            setActiveStep(0);
            setShowModal(false);
        }
        if (!!behavior) {
            CreateChatWithBehavior(behavior,version).then(handleChatCreation)
        }
        else {
            CreateChat(version, behavior).then(handleChatCreation)
        }
    };

    const closeModal = () => {
        setShowModal(false);
    };
    return (
        <Dialog open={showModal} onClose={closeModal} PaperProps={{ sx: DialogStyle.paper }}>
            <DialogTitle sx={{ color: 'white' }}>Chat Setup</DialogTitle>
            <DialogContent sx={DialogStyle.stepperContainer}>
                <Stepper activeStep={activeStep} sx={DialogStyle.stepper} alternativeLabel>
                    {steps.map((label, index) => (
                        <Step key={label}>
                            <StepLabel>
                                <Typography sx={{ color: 'white' }}>
                                    {label}
                                </Typography>
                            </StepLabel>
                        </Step>
                    ))}
                </Stepper>
                {activeStep === 0 && (
                    <Box sx={DialogStyle.stepContents}>
                        <Typography sx={{ color: 'white' }}>Choose Version</Typography>
                        <FormControlLabel
                            control={
                                <Switch
                                    checked={version === '4.0'}
                                    onChange={toggleStep}
                                    color="primary"
                                />
                            }
                            label={version}
                        />
                    </Box>
                )}
                {activeStep === 1 && (
                    <Box sx={DialogStyle.stepContents}>
                        <Typography sx={{ color: 'white', marginBottom: '5px' }}>Define Behavior</Typography>
                        <TextField
                            sx={inputStyle}
                            label="Behavior"
                            variant="outlined"
                            fullWidth
                            value={behavior}
                            onChange={(e) => setBehavior(e.target.value)}
                        />
                    </Box>
                )}
            </DialogContent>
            <DialogActions>
                {activeStep === 0 ? (
                    <Button onClick={closeModal}>Cancel</Button>
                ) : (
                    <Button onClick={handleBack}>Back</Button>
                )}
                {activeStep === steps.length - 1 ? (
                    <Button onClick={createChat}>Create Chat</Button>
                ) : (
                    <Button onClick={handleNext}>Next</Button>
                )}
            </DialogActions>
        </Dialog>
        )
}