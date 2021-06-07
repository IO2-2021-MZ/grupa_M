import React, { useState, useContext, useEffect } from "react";
import SnackbarContext from "../contexts/SnackbarContext";
import LoadingContext from "../contexts/LoadingContext";
import axios from "axios";
import { makeStyles } from "@material-ui/core/styles";
import Typography from "@material-ui/core/Typography";
import AppBar from "@material-ui/core/AppBar";
import { CardMedia, Container, CssBaseline } from "@material-ui/core";
import ArrowBackIcon from "@material-ui/icons/ArrowBack";
import Toolbar from "@material-ui/core/Toolbar";
import Button from "@material-ui/core/Button";
import Grid from "@material-ui/core/Grid";
import Card from "@material-ui/core/Card";
import CardContent from "@material-ui/core/CardContent";
import CardActions from "@material-ui/core/CardActions";
import TextField from "@material-ui/core/TextField";
import InputLabel from "@material-ui/core/InputLabel";
import MenuItem from "@material-ui/core/MenuItem";
import FormHelperText from "@material-ui/core/FormHelperText";
import FormControl from "@material-ui/core/FormControl";
import Select from "@material-ui/core/Select";
import apiUrl from "../shared/apiURL";
import UserContext from "../contexts/UserContext";
import HomeIcon from "@material-ui/icons/Home";
import Rating from "@material-ui/lab/Rating";
import StarBorderIcon from "@material-ui/icons/StarBorder";
import Box from "@material-ui/core/Box";
import { Link as RouterLink } from "react-router-dom";
import headers from "../shared/authheader"

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
    display: "flex",
    flexDirection: "column",
    color: "default",
  },
  cardMedia: {},
  cardContent: {
    flexGrow: 1,
    color: "default",
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

export default function AllUsers() {
  const classes = useStyles();

  
  const { setLoading } = useContext(LoadingContext);
  const { setSnackbar } = useContext(SnackbarContext);

  async function fetchData() {
    setLoading(true);
    setLoading(false);
  }

  

  

  useEffect(() => {
    fetchData();
  });

  return (
    <React.Fragment>
      <CssBaseline />
      <AppBar>
        <Toolbar>
          <Button>
            <RouterLink
              to={"/RestaurantList"}
              style={{ color: "#FFF" }}
            >
              <ArrowBackIcon fontSize="large" />
            </RouterLink>
          </Button>
          
        </Toolbar>
      </AppBar>
      <div className={classes.heroContent}>
        <Container className={classes.cardGrid} maxWidth="md">
          <Grid container spacing={4} alignItems="center">
            <Grid xs={12}>
              <Card>
                <CardContent>
                <Typography variant="h4" color="inherit" noWrap>
                    Show all useres of type:
                </Typography>
                    <br/>
                        <Button variant="contained" color="primary">
                            <RouterLink to={"/Users/"+"employee"}  style={{ color: "#FFF" }}>
                                Employees
                            </RouterLink>
                        </Button>
                        &nbsp;
                        &nbsp;
                        <Button variant="contained" color="primary">
                            <RouterLink to={"/Users/"+"customer"}  style={{ color: "#FFF" }}>
                                Customers
                            </RouterLink>
                        </Button>
                        &nbsp;
                        &nbsp;
                        <Button variant="contained" color="primary">
                            <RouterLink to={"/Users/"+"admin"}  style={{ color: "#FFF" }}>
                                Admins
                            </RouterLink>
                        </Button>
                </CardContent>
                
                        
              </Card>
            </Grid>
          </Grid>
        </Container>
      </div>
    </React.Fragment>
  );
}
