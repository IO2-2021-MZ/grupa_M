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
import { useLocation } from "react-router-dom";
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Paper from '@material-ui/core/Paper';
import apiUrl from "../shared/apiURL"
import headers from "../shared/authheader";
import UserContext from "../contexts/UserContext"

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
    const { setLoading } = useContext(LoadingContext);
    const { user, setUser } = useContext(UserContext);
    const [added, setAdded] = useState(false);
    const {restId} = props;

    const [paymentMethod, setPaymentMethod] = useState("");
    const [date, setDate] = useState("");
    const [street, setStreet] = useState("");
    const [city, setCity] = useState("");
    const [postCode, setPostCode] = useState("");
    const [discountCodeString, setDiscountCodeString] = useState("");
    const [discountCode, setDiscountCode] = useState({id: null});

    let orders = useLocation().state.orders;
    var originalPrice = orders.map(order => order.position.price).reduce((a, b) => a + b);
    const [finalPrice, setFinalPrice] = useState(originalPrice);
    var positionsIds = orders.map(order => order.position.id);

    const handlePaymentMethodChange = (event) => {
        setPaymentMethod(event.target.value);
    };

    const handleDateChange = (event) => {
        setDate(event.target.value);
    };

    const handleStreetChange = (event) => {
        setStreet(event.target.value);
    };

    const handleCityChange = (event) => {
        setCity(event.target.value);
    };

    const handlePostCodeChange = (event) => {
        setPostCode(event.target.value);
    };

    const handleDiscountCodeStringChange = (event) => {
        setDiscountCodeString(event.target.value);
    }

    const saveNewOrder = async () => {
        setLoading(true);
        var config;
        if(discountCode == undefined)
        {
            console.log("chuj")
            config = {
                method: 'post',
                url: apiUrl + "order",
                headers: headers(user),
                data: {
                    "paymentMethod": paymentMethod,
                    "date": "2021-05-23T18:30:16.966Z",
                    "address": {
                        "city": city,
                        "street": street,
                        "postCode": postCode
                    },
                    "customerId": user.id,
                    "restaurantId": restId,
                    "positionsId": positionsIds
                }
            };
        } else
        {
            config = {
                method: 'post',
                url: apiUrl + "order",
                headers: headers(user),
                data: {
                    "paymentMethod": paymentMethod,
                    "date": "2021-05-23T18:30:16.966Z",
                    "address": {
                        "city": city,
                        "street": street,
                        "postCode": postCode
                    },
                    "discountCodeId": discountCode.id,
                    "customerId": user.id,
                    "restaurantId": restId,
                    "positionsId": positionsIds
                }
            };
        } 

        const response = axios(config)
            .then(() => setLoading(false))
            .then(() => setAdded(true))
            .then(() => setLoading(false))
            .catch((error) => setSnackbar(error.message))
            .then(() => setLoading(false));
    }

    const validateDiscountCode = async () => {
        var config = {
            method: 'get',
            url: apiUrl + "discountCode?code=" + discountCodeString,
            headers: headers(user)
        };
        try
        {
          const response = await axios(config);
          setDiscountCode(response.data);
        }
        catch(error)
        {
          removeDiscountCode();
          setDiscountCode();
        }
    }

    const applyDiscountCode = () => {
        if(discountCode !== undefined)
        {
            setFinalPrice(originalPrice * (1 - discountCode.percent * 0.01));
        }
    }

    const removeDiscountCode = () =>{
        setFinalPrice(originalPrice);
    }

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
                                            label="Date"
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
                                    <TextField
                                        id="discountCode-multiline-static"
                                        label="DiscountCode"
                                        multiline
                                        defaultValue=""
                                        variant="outlined"
                                        fullWidth={true}
                                        onChange={handleDiscountCodeStringChange}
                                    />
                                    <Button variant="contained" color="primary" size="small" style={{ margin: 15 }} onClick={validateDiscountCode}>
                                        Validate Code
                                    </Button>
                                    <Button variant="contained" color="primary" size="small" style={{ margin: 15 }} onClick={applyDiscountCode}>
                                        Apply Discount Code
                                    </Button>
                                    <TableContainer component={Paper}>
                                        <Table className={classes.table} aria-label="simple table">
                                            <TableHead>
                                                <TableRow>
                                                    <TableCell align="right">Name</TableCell>
                                                    <TableCell align="right">Price</TableCell>
                                                </TableRow>
                                            </TableHead>
                                            <TableBody>
                                                {orders.map((row) => (
                                                    <TableRow key={row.countId}>
                                                        <TableCell align="right">{row.position.name}</TableCell>
                                                        <TableCell align="right">{row.position.price}</TableCell>
                                                    </TableRow>
                                                ))}
                                                <TableRow>
                                                    <TableCell allign="right">Original price</TableCell>
                                                    <TableCell allign="right">{originalPrice}</TableCell>
                                                </TableRow>
                                                <TableRow>
                                                    <TableCell allign="right">Final price</TableCell>
                                                    <TableCell allign="right">{finalPrice}</TableCell>
                                                </TableRow>
                                            </TableBody>
                                        </Table>
                                    </TableContainer>
                                </CardContent>
                                <CardActions>
                                    <Button variant="contained" color="primary" size="small" style={{ margin: 15 }} onClick={saveNewOrder}>
                                        Submit order
                                    </Button>
                                </CardActions>
                            </Card>
                        </Grid>
                    </Grid>
                </Container>
            </div>
        </React.Fragment>
    )
}

export default AddNewOrder;