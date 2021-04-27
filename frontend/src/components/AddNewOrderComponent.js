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

const AddNewOrder = (props) => {
    const { setSnackbar } = useContext(SnackbarContext);
    const classes = useStyles();

    const [paymentMethod, setPaymentMethod] = useState("");
    const [date, setDate] = useState("");
    const [street, setStreet] = useState("");
    const [city, setCity] = useState("");
    const [postCode, setPostCode] = useState("");

    var discountCodeId = props.discountCodeId;
    var customerId = props.customerId;
    var restaurantId = props.restaurantId;

    const handlePaymentMethodChange = (event) => {
        setPaymentMethod(event.target.value);
    };

    const handleDateChange = (date) => {
        setDate(date);
    };

    const handleStreetChange = (s) => {
        setStreet(s);
    };

    const handleCityChange = (c) => {
        setCity(c);
    };

    const handlePostCodeChange = (p) => {
        setPostCode(p);
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
                        Make New Order
              </Typography>
                </Toolbar>
            </AppBar>
            <div className={classes.heroContent}>
                <Container className={classes.cardGrid} maxWidth="md">
                    <Grid container spacing={4} alignItems="center">
                        <Grid xs={12} >
                            <Card className={classes.card}>
                                <CardMedia className={classes.cardMedia} />
                                <CardContent className={classes.cardContent}>
                                    <Typography variant="h5" align="left" color="textPrimary">
                                        Make a new order
                        </Typography>
                                    <br />
                                    <FormControl className={classes.formControl}>
                                        <InputLabel id="payment-method-label">Payment method</InputLabel>
                                        <Select
                                            labelId="payment-method-select-label"
                                            id="payment-method-select"
                                            value={paymentMethod}
                                            onChange={handlePaymentMethodChange}
                                        >
                                            <MenuItem value={"Card"}>Card</MenuItem>
                                            <MenuItem value={"Transfer"}>Transfer</MenuItem>
                                        </Select>
                                    </FormControl>
                                    <br />
                                    <form className={classes.container} noValidate>
                                        <TextField
                                            id="datetime-local"
                                            label="Next appointment"
                                            type="datetime-local"
                                            defaultValue="2017-05-24T10:30"
                                            className={classes.textField}
                                            InputLabelProps={{
                                                shrink: true,
                                            }}
                                        />
                                    </form>
                                </CardContent>
                            </Card>
                            <Card>
                                <CardContent>
                                    <TextField
                                        id="street-multiline-static"
                                        label="Street"
                                        multiline
                                        defaultValue=""
                                        variant="outlined"
                                        fullWidth={true}
                                        onChange={handleStreetChange}
                                    />
                                    <TextField
                                        id="city-multiline-static"
                                        label="City"
                                        multiline
                                        defaultValue=""
                                        variant="outlined"
                                        fullWidth={true}
                                        onChange={handleCityChange}
                                    />
                                    <TextField
                                        id="PostCode-multiline-static"
                                        label="PostCode"
                                        multiline
                                        defaultValue=""
                                        variant="outlined"
                                        fullWidth={true}
                                        onChange={handlePostCodeChange}
                                    />
                                </CardContent>
                            </Card>
                        </Grid>
                    </Grid>
                </Container>
            </div>
        </React.Fragment>
    )
}

export default AddNewOrder;