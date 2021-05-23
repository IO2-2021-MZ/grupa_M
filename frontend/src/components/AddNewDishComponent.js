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
import {Link as RouterLink} from 'react-router-dom';
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

const AddNewDish = (props) => {
  const { setSnackbar } = useContext(SnackbarContext);
  const { user, setUser } = useContext(UserContext);
  const classes = useStyles();
  const [section, setSection] = useState();
  const [dishName, setDishName] = useState("");
  const [price, setPrice] = useState("");
  const [description, setDescription] = useState("");
  const [added,setAdded] = useState(false);

  const handleDishNameChange = (event) => {
    setDishName(event.target.value);
  };
  const handlePriceChange = (event) => {
    setPrice(event.target.value);
  };
  const handleDescriptionChange = (event) => {
    setDescription(event.target.value);
  };
  

  const { sectionId } = props;
  const saveNewDish = async () => {
    var config = {
      method: 'post',
      url: apiUrl + 'restaurant/menu/position?id='+sectionId,
      headers: headers(user),
      data:{
        "name": dishName,
        "description": description,
        "price": price,
      },
    };

    try {
      console.log({
        dishName,
      });
      await axios(config);
    } catch (e) {
      console.error(e);
      setSnackbar({
        open: true,
        message: "Error occured",
        type: "error",
      });
    }
    setAdded(true);
  };

  return (
    <div>
      { added ?
      <div>
      <Typography style={{margin:150}} variant="h4">Added succesfully!</Typography>
      <Button variant="contained" color="default" size="large">
          <RouterLink to={"/RestaurateurSections/"+localStorage.getItem('rest_id')}>
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
          <RouterLink to={"/RestaurateurSections/"+localStorage.getItem('rest_id')}>
            <ArrowBackIcon fontSize="large" />
            </RouterLink>
          </Button>
          <Typography variant="h6" color="inherit" noWrap>
            Make New Dish
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
                    Create a new dish
                  </Typography>
                  <TextField style={{margin:10}}
                    id="DishName-multiline-static"
                    label="Dish Name"
                    multiline
                    defaultValue=""
                    variant="outlined"
                    fullWidth={true}
                    onChange={handleDishNameChange}
                  />
                  <TextField style={{margin:10}}
                    id="price-multiline-static"
                    label="Price"
                    multiline
                    defaultValue=""
                    variant="outlined"
                    fullWidth={true}
                    onChange={handlePriceChange}
                  />
                  <TextField style={{margin:10}}
                    id="descrpition-multiline-static"
                    label="Description"
                    multiline
                    defaultValue=""
                    variant="outlined"
                    fullWidth={true}
                    onChange={handleDescriptionChange}
                  />
                  <br />
                  <br />
                  <Button
                    variant="contained"
                    color="primary"
                    onClick={() => saveNewDish()}
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

export default AddNewDish;
