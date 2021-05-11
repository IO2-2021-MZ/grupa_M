import { useState, useContext, useEffect } from "react";
import SnackbarContext from '../contexts/SnackbarContext';
import LoadingContext from '../contexts/LoadingContext';
import axios from 'axios';
import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Typography from '@material-ui/core/Typography';
import AppBar from '@material-ui/core/AppBar';
import { CardMedia, Container, CssBaseline } from "@material-ui/core";
import ArrowBackIcon from '@material-ui/icons/ArrowBack';
import Toolbar from '@material-ui/core/Toolbar';
import Button from '@material-ui/core/Button';
import Grid from '@material-ui/core/Grid';
import Card from '@material-ui/core/Card';
import CardContent from '@material-ui/core/CardContent';
import CardActions from '@material-ui/core/CardActions';
import TextField from '@material-ui/core/TextField';
import InputLabel from '@material-ui/core/InputLabel';
import MenuItem from '@material-ui/core/MenuItem';
import FormHelperText from '@material-ui/core/FormHelperText';
import FormControl from '@material-ui/core/FormControl';
import Select from '@material-ui/core/Select';

const useStyles = makeStyles((theme) => ({
    icon: {
        marginRight: theme.spacing(2),
    },
    heroContent: {
        backgroundColor: theme.palette.background.paper,
        padding: theme.spacing(8, 0, 6),
    },
    heroButtons: {
        marginTop: theme.spacing(4),
    },
    cardGrid: {
        paddingTop: theme.spacing(8),
        paddingBottom: theme.spacing(8),
    },
    card: {
        display: 'flex',
        flexDirection: 'column',
        color: "default"
    },
    cardMedia: {

    },
    cardContent: {
        flexGrow: 1,
        color: "default"
    },
    footer: {
        backgroundColor: theme.palette.background.paper,
        padding: theme.spacing(6),
    },
    formControl: {
        margin: theme.spacing(1),
        minWidth: 120,
    },
    selectEmpty: {
        marginTop: theme.spacing(2),
    },
}));

const AddNewDiscountCode = (props) => {
    const classes = useStyles();
    const { setSnackbar } = useContext(SnackbarContext);

    const [code, setCode] = useState("");
    const [dateFrom, setDateFrom] = useState("");
    const [dateTo, setDateTo] = useState("");

    var restaurantId = props.restaurantId;

    const handleCodeChange = (c) => {
        setCode(c);
    };

    const handleDateFromChange = (date) => {
        setDateFrom(date);
    };

    const handleDateToChange = (date) => {
        setDateTo(date);
    };

    return (
        <React.Fragment>
            <CssBaseline />
            <AppBar>
                <Toolbar>
                    <Button>
                        <ArrowBackIcon fontSize="large" />
                    </Button>
                    <Typography variant="h6" color="inherit" noWrap>
                        Add new discount code
              </Typography>
                </Toolbar>
            </AppBar>
            <div className={classes.heroContent}>
                <Container className={classes.cardGrid} maxWidth="md">
                    <Grid container spacing={4} alignItems="center">
                        <Grid xs={12} >
                            <Card className={classes.card}>
                                <CardMedia
                                    className={classes.cardMedia}
                                />
                                <CardContent className={classes.cardContent}>
                                    <Typography variant="h5" align="left" color="textPrimary">
                                        Add new discount code for restaurant: {props.restaurantId}
                                    </Typography>
                                    <br />
                                    <TextField
                                        id="outlined-multiline-static"
                                        label="Code"
                                        multiline
                                        defaultValue=""
                                        variant="outlined"
                                        fullWidth={true}
                                        onChange={handleCodeChange}
                                    />
                                    <br />
                                    <form className={classes.container} noValidate>
                                        <TextField
                                            id="dateFrom-local"
                                            label="Date From"
                                            type="datetime-local"
                                            defaultValue="2017-05-24T10:30"
                                            className={classes.textField}
                                            InputLabelProps={{
                                                shrink: true,
                                            }}
                                            onChange={handleDateFromChange}
                                        />
                                    </form>
                                    <form className={classes.container} noValidate>
                                        <TextField
                                            id="dateTo-local"
                                            label="Date To"
                                            type="datetime-local"
                                            defaultValue="2017-05-24T10:30"
                                            className={classes.textField}
                                            InputLabelProps={{
                                                shrink: true,
                                            }}
                                            onChange={handleDateToChange}
                                        />
                                    </form>
                                </CardContent>
                            </Card>
                        </Grid>
                    </Grid>
                </Container>
            </div>
        </React.Fragment>
    )
}

export default AddNewDiscountCode;