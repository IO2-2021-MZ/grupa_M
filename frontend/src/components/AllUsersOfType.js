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
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Paper from '@material-ui/core/Paper';
import headers from "../shared/authheader";

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
      height: '100%',
      display: 'flex',
      flexDirection: 'column',
    },
    cardMedia: {
      paddingTop: '56.25%', // 16:9
    },
    cardContent: {
      flexGrow: 1,
    },
    footer: {
      backgroundColor: theme.palette.background.paper,
      padding: theme.spacing(6),
    },
  }));

function AllUsersOfType(props) {
    
    
    const classes = useStyles();

    const { type } = props;
    const { setLoading } = useContext(LoadingContext);
    const { setSnackbar } = useContext(SnackbarContext);

    const [usersList, setUsersList] = useState([]);
    const {user, setUser} = useContext(UserContext);

  
    async function fetchData() {
      setLoading(true);
  
      var config = {
        method: 'get',
        url: apiUrl + "user/"+type+"/all",
        headers: headers(user)
      };
      
      try
      {
        const response = await axios(config);
        console.log(response.data);
        setUsersList(response.data);
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
      }, [setUsersList]);
  
      const deleteUser = (id) => {
        setLoading(true);
  
        var config = {
          method: 'delete',
          url: apiUrl + "user/"+type+"?id=" + id,
          headers: headers(user)
        };
        
        const response =  axios(config)
        .then(() => fetchData())
        .catch((error) => setSnackbar(error.message));
        
      }
    
    return (
        <div>
        <Typography color="primary" variant="h4">
            List of all {type}
        </Typography>
        <Button variant="contained" color="default">
            <RouterLink to="/AllUsers">
            Back
            </RouterLink>
        </Button>
        <TableContainer component={Paper}>
            <Table className={classes.table} aria-label="simple table">
                <TableHead>
                <TableRow>
                    {console.log(usersList)}
                    <TableCell>User role</TableCell>
                    <TableCell align="right">Name</TableCell>
                    <TableCell align="right">Surname</TableCell>
                    <TableCell align="right">Email</TableCell>
                    <TableCell align="right"></TableCell>
                </TableRow>
                </TableHead>
                <TableBody>
                {usersList.map((row) => (
                    <TableRow key={row.id}>
                    <TableCell component="th" scope="row">
                        {row.role}
                    </TableCell>
                    <TableCell align="right">{row.name}</TableCell>
                    <TableCell align="right">{row.surname}</TableCell>
                    <TableCell align="right">{row.email}</TableCell>
                    <TableCell align="right">
                        <Button variant="contained" color="secondary" onClick = {() => deleteUser(row.id)}>
                            X
                        </Button>
                    </TableCell>
                    </TableRow>
                ))}
                </TableBody>
            </Table>
        </TableContainer>
        </div>
    )
}

export default AllUsersOfType
