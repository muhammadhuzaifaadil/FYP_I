import React, { useEffect, useState } from 'react';
import { Box, Container, Typography, Button, LinearProgress, Stepper, Step, StepLabel, StepContent } from '@mui/material';
import ConsultationResponse from './ConsultationResponse';

const API_URL = 'https://localhost:7181/GetQuestion';
const POST_URL = 'https://localhost:7181/GetConsultation';

const VerticalLinearStepper = () => {
    const [steps, setSteps] = useState([]);
    const [activeStep, setActiveStep] = useState(0);
    const [activeForm, setActiveForm] = useState(0);
    const [selectedOptions, setSelectedOptions] = useState([]);
    const [finalAnswers, setFinalAnswers] = useState([]);
    const [showResponse, setShowResponse] = useState(false);
    const [responseText, setResponseText] = useState('');
    const [errorText, setErrorText] = useState('');

    useEffect(() => {
        fetch(API_URL)
            .then((response) => response.json())
            .then((data) => {
                const fetchedSteps = data.map((questionData) => {
                    return {
                        label: `Step ${questionData.questionID}`,
                        forms: [
                            {
                                question: questionData.questions,
                                heading: questionData.questionsCategory,
                                options: [
                                    questionData.optionOne,
                                    questionData.optionTwo,
                                    questionData.optionThree,
                                    questionData.optionFour
                                ]
                            }
                        ]
                    };
                });
                setSteps(fetchedSteps);
                setSelectedOptions(Array(fetchedSteps.length).fill({}));
                setFinalAnswers(Array(fetchedSteps.length).fill(null));
            })
            .catch((error) => {
                console.error('Error fetching questions:', error);
                setErrorText('Failed to fetch questions. Please try again.');
            });
    }, []);

    const calculateProgress = () => {
        const totalQuestions = steps.reduce((total, step) => total + (step.forms ? step.forms.length : 0), 0);
        if (totalQuestions === 0) return 0;

        const answeredQuestions = (activeStep * (steps[activeStep]?.forms?.length || 0)) + activeForm + 1;
        return (answeredQuestions / totalQuestions) * 100;
    };

    const handleNext = () => {
        if (activeForm < (steps[activeStep]?.forms?.length || 0) - 1) {
            setActiveForm((prevActiveForm) => prevActiveForm + 1);
        } else {
            if (activeStep === steps.length - 1 && activeForm === (steps[activeStep]?.forms?.length || 0) - 1) {
                submitAnswers();
            } else {
                setActiveStep((prevActiveStep) => prevActiveStep + 1);
                setActiveForm(0);
            }
        }
    };

    const handleBack = () => {
        if (activeForm > 0) {
            setActiveForm((prevActiveForm) => prevActiveForm - 1);
        } else {
            setActiveStep((prevActiveStep) => prevActiveStep - 1);
            setActiveForm((steps[activeStep - 1]?.forms?.length || 0) - 1);
        }
    };

    const handleOptionSelect = (option) => {
        const updatedSelectedOptions = [...selectedOptions];
        updatedSelectedOptions[activeStep] = { ...selectedOptions[activeStep] };
        updatedSelectedOptions[activeStep][activeForm] = option;
        setSelectedOptions(updatedSelectedOptions);

        const answerDetails = {
            category: steps[activeStep]?.forms[activeForm].heading,
            question: steps[activeStep]?.forms[activeForm].question,
            answer: option
        };
        const updatedFinalAnswers = [...finalAnswers];
        updatedFinalAnswers[activeStep] = answerDetails;
        setFinalAnswers(updatedFinalAnswers);
    };

    const renderOptions = (options) => {
        return options.map((option, index) => (
            <Button
                key={index}
                variant={selectedOptions[activeStep]?.[activeForm] === option ? 'contained' : 'outlined'}
                onClick={() => handleOptionSelect(option)}
                sx={{ mt: 1, mr: 1 }}
            >
                {option}
            </Button>
        ));
    };

    const progress = calculateProgress();

    const submitAnswers = async () => {
        try {
            const formattedAnswers = finalAnswers
                .filter((answer) => answer !== null)
                .map((answer) => {
                    if (answer && answer.category && answer.question && answer.answer) {
                        return `Category: ${answer.category}, Question: ${answer.question}, Answer: ${answer.answer}`;
                    } else {
                        return ''; // Handle invalid answer object
                    }
                })
                .filter((formattedAnswer) => formattedAnswer !== '')
                .join('\n');
    
            if (formattedAnswers === '') {
                setErrorText('No valid answers to submit.');
                setShowResponse(true);
                return;
            }
    
            const fullResponse = `Give the name of only 3 fields I have to choose for the graduation based on the data I have provided\n${formattedAnswers}`;
    
            const response = await fetch(POST_URL, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ answers: fullResponse })
            });
    
            if (response.ok) {
                const responseData = await response.json();
                setResponseText(responseData); // Assuming the response data is a string
                setShowResponse(true);
                setErrorText(''); // Clear any previous error message
            } else {
                const errorData = await response.text();
                setErrorText(`Error: ${errorData}`);
                setShowResponse(true);
            }
        } catch (error) {
            console.error('Error submitting consultation data:', error);
            setErrorText(`Error: ${error.message}`);
            setShowResponse(true);
        }
    };

    const handleRetry = () => {
        setActiveStep(0);
        setActiveForm(0);
        setFinalAnswers([]);
        setSelectedOptions([]);
        setShowResponse(false);
        setResponseText('');
        setErrorText('');
    };

    const handleBackToHome = () => {
        // Implement navigation logic to go back to the home page
        console.log('Navigate back to home page');
    };

    return (
        <Box>
            <div className="stepper-main">
                <div className="stepper-main-inner">
                    <Container maxWidth="xl">
                        <Stepper activeStep={activeStep} orientation="vertical">
                            {steps.map((step, index) => (
                                <Step key={step.label}>
                                    <StepLabel>{step.label}</StepLabel>
                                    <StepContent>
                                        {step.forms && step.forms[activeForm] && (
                                            <>
                                                <Typography className="step-main-question" variant="h4">{step.forms[activeForm].question}</Typography>
                                                <Typography className="step-question" variant="h6">{step.forms[activeForm].heading}</Typography>
                                                <div className="step-question-option">
                                                    <Box sx={{ mb: 2 }}>
                                                        {renderOptions(step.forms[activeForm].options)}
                                                        <div className="sq-question-button">
                                                            {index > 0 || activeForm > 0 ? (
                                                                <Button
                                                                    onClick={handleBack}
                                                                    sx={{ mt: 1, mr: 1 }}
                                                                >
                                                                    Back
                                                                </Button>
                                                            ) : null}
                                                            <Button
                                                                variant="contained"
                                                                onClick={handleNext}
                                                                sx={{ mt: 1, mr: 1 }}
                                                            >
                                                                {index === steps.length - 1 && activeForm === (step.forms?.length || 0) - 1 ? 'Finish' : 'Continue'}
                                                            </Button>
                                                        </div>
                                                    </Box>
                                                </div>
                                            </>
                                        )}
                                    </StepContent>
                                </Step>
                            ))}
                        </Stepper>
                        {showResponse && (
                            <ConsultationResponse
                                responseText={errorText || responseText}
                                onRetry={handleRetry}
                                onBackToHome={handleBackToHome}
                                isError={!!errorText}
                            />
                        )}
                    </Container>
                </div>
                <div className="cust-progress-bar">
                    <LinearProgress variant="determinate" value={progress} />
                </div>
            </div>
        </Box>
    );
};

export default VerticalLinearStepper;
