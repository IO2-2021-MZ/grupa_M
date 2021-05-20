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

    const [restId, setRestId] = useState(0);
    const [dateFrom, setDateFrom] = useState('');
    const [dateTo, setDateTo] = useState('');
    const [code, setCode] = useState('');
    const [percent, setPercent] = useState(0);
    
    const handleChange = (event) => {
        setRestId(event.target.value);
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
       <FormControl style={{margin: 10}} variant="outlined" className={classes.formControl}>
           <TextField variant="outlined" label="Code"></TextField><br/>
           <TextField variant="outlined" type="date" label="Date From" defaultValue={new Date().toJSON().slice(0,10).replace(/-/g,'-')}>Date From</TextField><br/>
           <TextField variant="outlined" type="date" label="Date To" defaultValue={new Date().toJSON().slice(0,10).replace(/-/g,'-')}>Date To</TextField><br/>
            <Select variant="outlined"
                value={restId}
                onChange={handleChange}>
                {rests.map(el => 
                    <MenuItem value={el.id}>
                        {el.name}
                    </MenuItem>
                )}
            </Select><br/>
           <TextField variant="outlined" label="Percent">Percent</TextField><br/>
           <Button color="primary">Save</Button>
       </FormControl> 
    )
}

export default NewRabatCode
