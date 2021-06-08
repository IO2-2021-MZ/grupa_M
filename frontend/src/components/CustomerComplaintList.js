import React, { useState, useEffect, useContext } from 'react';
import AppBar from '@material-ui/core/AppBar';
import Button from '@material-ui/core/Button';
import Toolbar from '@material-ui/core/Toolbar';
import Typography from '@material-ui/core/Typography';
import { makeStyles } from '@material-ui/core/styles';
import SnackbarContext from '../contexts/SnackbarContext';
import LoadingContext from '../contexts/LoadingContext';
import axios from 'axios';
import apiUrl from "../shared/apiURL"
import UserContext from "../contexts/UserContext"
import { Link as RouterLink } from 'react-router-dom';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Paper from '@material-ui/core/Paper';
import ArrowBackIcon from "@material-ui/icons/ArrowBack";
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

function CustomerComplaintHistory(props) {
    
    const classes = useStyles();
    const { restId } = props;
    const { setLoading } = useContext(LoadingContext);
    const { setSnackbar } = useContext(SnackbarContext);
    const [complaints, setComplaints] = useState([]);
    const {user, setUser} = useContext(UserContext);

  
    async function fetchData() {
      setLoading(true);
      var config = {
        method: 'get',
        url: apiUrl + "user/customer/complaint/all",
        headers: headers(user)
      };
      
      try
      {
        const response = await axios(config);
        setComplaints(response.data);
        console.log(response.data);
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
      }, [setComplaints]);
  
      
    
    return (
        <React.Fragment>
        <AppBar position="relative">
        <Toolbar>
          <Button>
            <RouterLink
              to={"/RestaurantList"}
              style={{ color: "#FFF" }}
            >
              <ArrowBackIcon fontSize="large" />
            </RouterLink>
          </Button>
          <Typography variant="h6" color="inherit" noWrap>
            List of complaints view
          </Typography>
        </Toolbar>
      </AppBar>
        <div>
        <Typography color="primary" variant="h4">
          List of all complaints for customer
        </Typography>
        <TableContainer component={Paper}>
            <Table className={classes.table} aria-label="simple table">
                <TableHead>
                <TableRow>
                    {console.log(complaints)}
                    <TableCell>Order id</TableCell>
                    <TableCell align="center">State</TableCell>
                    <TableCell align="center">Content</TableCell>
                    <TableCell align="center">Response</TableCell>
                    <TableCell align="right"></TableCell>
                </TableRow>
                </TableHead>
                <TableBody>
                {complaints.map((row) => (
                    <TableRow key={row.id}>
                    <TableCell component="th" scope="row">
                        {row.id}
                    </TableCell>
                    <TableCell align="center">{row.open ? 'Open' : 'Closed'}</TableCell>
                    <TableCell align="center">{row.content}</TableCell>
                    <TableCell align="center">{row.response == null ? 'no respond' : row.response}</TableCell>
                    <TableCell align="right">  
                    </TableCell>
                    </TableRow>
                ))}
                </TableBody>
            </Table>
        </TableContainer>
        </div>
        </React.Fragment>
    )
}

export default CustomerComplaintHistory
