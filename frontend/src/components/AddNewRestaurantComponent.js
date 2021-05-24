import { useState, useContext, useEffect } from "react";
import SnackbarContext from "../contexts/SnackbarContext";
import LoadingContext from "../contexts/LoadingContext";
import axios from "axios";
import React from "react";
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
import {Link as RouterLink} from 'react-router-dom'
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

const AddNewRestaurant = (props) => {
  const { setSnackbar } = useContext(SnackbarContext);
  const classes = useStyles();
  const { user, setUser } = useContext(UserContext);

  const [restaurantName, setRestaurantName] = useState("");
  const [street, setStreet] = useState("");
  const [city, setCity] = useState("");
  const [postCode, setPostCode] = useState("");
  const [email, setEmail] = useState("");
  const [added, setAdded] = useState(false);

  const handleRestaurantNameChange = (p) => {
    setRestaurantName(p.target.value);
  };
  const handleStreetChange = (s) => {
    setStreet(s.target.value);
  };

  const handleCityChange = (c) => {
    setCity(c.target.value);
  };

  const handlePostCodeChange = (p) => {
    setPostCode(p.target.value);
  };
  const handleEmailChange = (p) => {
    setEmail(p.target.value);
  };

  const saveNewRestaurant = async () => {
    var config = {
      method: "post",
      url: apiUrl + "restaurant",
      headers: headers(user),
      data: {
        "name": restaurantName,
        "contactInformation": email,
        "address": {
          "street": street,
          "city": city,
          "postCode": postCode,
        },
      },
    };

    try {
      console.log({
        name: restaurantName,
        contactInformation: email,
        address: {
          street: street,
          city: city,
          postCode: postCode,
        },
      });
      await axios(config);
      setAdded(true);
    } catch (e) {
      console.error(e);
      setSnackbar({
        open: true,
        message: "Error occured",
        type: "error",
      });
    }
  };

  return (
    <div> 
      { added ?
      <div>
      <Typography style={{margin:150}} variant="h4">Added succesfully!</Typography>
      <Button variant="contained" color="default" size="large">
          <RouterLink to="/RestaurantList">
              Back
          </RouterLink>
      </Button>
  </div>
      :
    <React.Fragment>
      <CssBaseline />
      <AppBar>
        <Toolbar>
          <Button>
            <RouterLink to ="/RestaurantList">
            <ArrowBackIcon fontSize="large" />
            </RouterLink>
          </Button>
          <Typography variant="h6" color="inherit" noWrap>
            Make New Restaurant
          </Typography>
        </Toolbar>
      </AppBar>
      <div className={classes.heroContent}>
        <Container className={classes.cardGrid} maxWidth="md">
          <Grid container spacing={4} alignItems="center">
            <Grid xs={12}>
              <Card>
                <CardContent>
                  <Typography variant="h5" align="left" color="textPrimary">
                    Create new restaurant
                  </Typography>
                  <TextField style={{margin:10}}
                    id="restaurantName-multiline-static"
                    label="Restaurant Name"
                    multiline
                    defaultValue=""
                    variant="outlined"
                    fullWidth={true}
                    onChange={handleRestaurantNameChange}
                  />
                  <TextField style={{margin:10}}
                    id="email-multiline-static"
                    label="Email"
                    multiline
                    defaultValue=""
                    variant="outlined"
                    fullWidth={true}
                    onChange={handleEmailChange}
                  />
                </CardContent>
              </Card>
              <Card>
                <CardContent>
                  <TextField style={{margin:10}}
                    id="street-multiline-static"
                    label="Street"
                    multiline
                    defaultValue=""
                    variant="outlined"
                    fullWidth={true}
                    onChange={handleStreetChange}
                  />
                  <TextField style={{margin:10}}
                    id="city-multiline-static"
                    label="City"
                    multiline
                    defaultValue=""
                    variant="outlined"
                    fullWidth={true}
                    onChange={handleCityChange}
                  />
                  <TextField style={{margin:10}}
                    id="PostCode-multiline-static"
                    label="PostCode"
                    multiline
                    defaultValue=""
                    variant="outlined"
                    fullWidth={true}
                    onChange={handlePostCodeChange}
                  />
                  <br />
                  <br />
                  <Button style={{margin:10}}
                    variant="contained"
                    color="primary"
                    onClick={() => saveNewRestaurant()}
                  >
                    <Typography variant="button" color="inherit">
                      Save
                    </Typography>
                  </Button>
                </CardContent>
              </Card>
            </Grid>
          </Grid>
        </Container>
      </div>
    </React.Fragment>
}
    </div>
  );
};

export default AddNewRestaurant;
