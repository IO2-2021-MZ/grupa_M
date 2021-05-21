import React, { useState, useEffect, useContext } from 'react';
import AppBar from '@material-ui/core/AppBar';
import Button from '@material-ui/core/Button';
import Card from '@material-ui/core/Card';
import CardActions from '@material-ui/core/CardActions';
import CardContent from '@material-ui/core/CardContent';
import CardMedia from '@material-ui/core/CardMedia';
import CssBaseline from '@material-ui/core/CssBaseline';
import Grid from '@material-ui/core/Grid';
import Toolbar from '@material-ui/core/Toolbar';
import Typography from '@material-ui/core/Typography';
import { makeStyles } from '@material-ui/core/styles';
import Container from '@material-ui/core/Container';
import HomeIcon from '@material-ui/icons/Home';
import SnackbarContext from '../contexts/SnackbarContext';
import LoadingContext from '../contexts/LoadingContext';
import axios from 'axios';
import TextField from '@material-ui/core/TextField';
import apiUrl from "../shared/apiURL"
import UserContext from "../contexts/UserContext"
import Rating from '@material-ui/lab/Rating';
import StarBorderIcon from '@material-ui/icons/StarBorder';
import Box from '@material-ui/core/Box';
import { Link as RouterLink } from 'react-router-dom';
import { FormControl, InputLabel } from '@material-ui/core';
import { getAllByPlaceholderText } from '@testing-library/dom';
import MenuItem from '@material-ui/core/MenuItem';
import FormHelperText from '@material-ui/core/FormHelperText';
import Select from '@material-ui/core/Select';
import { white } from 'material-ui/styles/colors';
import { black } from 'material-ui/styles/colors';
import { Filter9 } from '@material-ui/icons';

const useStyles = makeStyles((theme) => ({
    formControl: {
      margin: theme.spacing(1),
      minWidth: 120,
      colo: black
    },
    selectEmpty: {
      marginTop: theme.spacing(2),
    },
  }));

function NewRabatCode() {
    const classes = useStyles();
    const { setLoading } = useContext(LoadingContext);
    const { setSnackbar } = useContext(SnackbarContext);
    const [rests, setRests] = useState([]);
    const {user, setUser} = useContext(UserContext);
    const [added, setAdded] = useState(false);
    const [restId, setRestId] = useState(0);
    const [dateFrom, setDateFrom] = useState(new Date().toJSON().slice(0,10).replace(/-/g,'-'));
    const [dateTo, setDateTo] = useState(new Date().toJSON().slice(0,10).replace(/-/g,'-'));
    const [code, setCode] = useState("");
    const [percent, setPercent] = useState("");
    const [valid, setValid] = useState();
    const handleChange = (event) => {
        setRestId(event.target.value);
        console.log(event.target.value)
      };

      const handleChangeDf = (event) => {
        setDateFrom(event.target.value);
      };
      const handleChangeDt = (event) => {
        setDateTo(event.target.value);
      };
      const handleChangeC = (event) => {
        setCode(event.target.value);
      };
      const handleChangeP = (event) => {
        setPercent(event.target.value);

      };

      const Validate = () => {
        let val = true; 
        let message = ""; 
        if(dateFrom.localeCompare(dateTo) > 0){
            val = false;
            message = message + "Date to must be greater then date from!\n"
        }
        if(restId == 0){
            val = false;
            message = message + "Select restaurant!\n"
        }
        if(code == "" || code === undefined) {
            val = false;
            message = message + "Provide code!\n"
        }
        if(isNaN(parseInt(percent))){
            val = false;
            message = message + "Provide correct percent of discount!\n"
        }
        
        if(message !== "") setValid(message);

        return val;
            
        }

      const addCode = () => {

        if(!Validate()) return;
        
        setLoading(true);
        var config = {
          method: 'post',
          url: apiUrl + "discountCode",
          headers: { 
            'Authorization': 'Bearer ' + user.token
          },
          data:{
              "code": code,
              "dateFrom": dateFrom+"T01:00:00.000Z",
              "dateTo": dateTo+"T01:00:00.000Z",
              "restaurantId": restId,
              "percent": percent
          }
        };
        const response =  axios(config)
        .then(() => fetchData())
        .then(() => setAdded(true))
        .then(() => setLoading(false))
        .catch((error) => setSnackbar(error.message))
        .then(() => setLoading(false));
      }

    async function fetchData() {
      setLoading(true);
  
      var config = {
        method: 'get',
        url: apiUrl + "restaurant/all",
        headers: { 
          'Authorization': 'Bearer ' + user.token
        }
      };
      
      try
      {
        const response = await axios(config);
        setRests(response.data);        
      }
      catch(error)
      {
        console.error(error);
                  setSnackbar({
                      open: true,
                      message: "Loading data failed",
                      type: "error"
                  });
      }
      
      setLoading(false);
      }
  
      useEffect(() => {
          fetchData();
      }, [setRests]);

    return (
        <div>
            { added ? 
            <div>
                <Typography style={{margin:150}} variant="h4">Added succesfully!</Typography>
                <Button variant="contained" color="default" size="large">
                    <RouterLink to="RabatCodeList">
                        Back
                    </RouterLink>
                </Button>
            </div>
            :
            <div style={{padding:150}}>
                <Typography color="default" variant="h4">Add new discount code!</Typography>
       <FormControl  variant="outlined" className={classes.formControl}>
           <TextField onChange={handleChangeC} variant="outlined" label="Code"></TextField><br/>
           <TextField onChange={handleChangeDf} variant="outlined" type="date" label="Date From" defaultValue={new Date().toJSON().slice(0,10).replace(/-/g,'-')}>Date From</TextField><br/>
           <TextField onChange={handleChangeDt} variant="outlined" type="date" label="Date To" defaultValue={new Date().toJSON().slice(0,10).replace(/-/g,'-')}>Date To</TextField><br/>
            <Select variant="outlined"
                value={restId}
                onChange={handleChange}>
                {rests.map(el => 
                    <MenuItem value={el.id}>
                        {el.name}
                    </MenuItem>
                )}
            </Select><br/>
           <TextField onChange={handleChangeP} variant="outlined" label="Percent">Percent</TextField><br/>
           <Button style={{margin:5}} variant="contained" onClick={addCode} color="default">Save</Button>
           <Button style={{margin:5}} variant="contained" color="default">
                    <RouterLink to="RabatCodeList">
                        Back
                    </RouterLink>
                </Button>
           { valid ? <Typography color="error">{valid}</Typography> : <></>}
       </FormControl> 
       </div>
        }
       </div>
    )
}

export default NewRabatCode
