import React, { useState, useEffect, useContext } from "react";
import AppBar from "@material-ui/core/AppBar";
import Button from "@material-ui/core/Button";
import Card from "@material-ui/core/Card";
import CardActions from "@material-ui/core/CardActions";
import CardContent from "@material-ui/core/CardContent";
import CardMedia from "@material-ui/core/CardMedia";
import CssBaseline from "@material-ui/core/CssBaseline";
import Grid from "@material-ui/core/Grid";
import Toolbar from "@material-ui/core/Toolbar";
import Typography from "@material-ui/core/Typography";
import { makeStyles, withStyles } from "@material-ui/core/styles";
import Container from "@material-ui/core/Container";
import Link from "@material-ui/core/Link";
import HomeIcon from "@material-ui/icons/Home";
import SnackbarContext from "../contexts/SnackbarContext";
import LoadingContext from "../contexts/LoadingContext";
import axios from "axios";
import { purple } from "@material-ui/core/colors";
import { Link as RouterLink } from "react-router-dom";
import TextField from "@material-ui/core/TextField";
import apiUrl from "../shared/apiURL";
import UserContext from "../contexts/UserContext";
import ArrowBackIcon from "@material-ui/icons/ArrowBack";

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
    height: "100%",
    display: "flex",
    flexDirection: "column",
  },
  cardMedia: {
    paddingTop: "56.25%", // 16:9
  },
  cardContent: {
    flexGrow: 1,
  },
  footer: {
    backgroundColor: theme.palette.background.paper,
    padding: theme.spacing(6),
  },
}));

const ColorButton = withStyles((theme) => ({
  root: {
    color: theme.palette.getContrastText(purple[500]),
    backgroundColor: purple[500],
    "&:hover": {
      backgroundColor: purple[700],
    },
  },
}))(Button);

export default function SectionsList() {
  //const restaurantId;
  const classes = useStyles();
  const { setLoading } = useContext(LoadingContext);
  const { setSnackbar } = useContext(SnackbarContext);
  const [sections, setSections] = useState([]);

  async function fetchData() {
    setLoading(true);
    var config = {
      method: "get",
      // url: "https://localhost:5001/restaurant/menu?id=1",
      // headers: {
      //   Authorization:
      //     "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjEiLCJuYmYiOjE2MjE1OTEyNjQsImV4cCI6MTYyMTU5MjE2NCwiaWF0IjoxNjIxNTkxMjY0fQ.x0qPt7mmAiPRTeUCVUOt_rTefNqztOykUmNHP1NQFds",
      // },
      url: apiUrl + "restaurant/menu?id=" + restaurantId,
      headers: {
        Authorization: "Bearer " + user.token,
      },
    };

    try {
      const response = await axios(config);
      //console.log(response);
      setSections(response.data);
    } catch (error) {
      console.error(error);
      setSnackbar({
        open: true,
        message: "Loading data failed",
        type: "error",
      });
    }
    setLoading(false);
  }

  useEffect(() => {
    fetchData();
  }, [setSections]);

  return (
    <React.Fragment>
      <AppBar position="relative">
        <Toolbar>
          {/* <Link to="/RestaurerRestaurantList"> */}
          <Button>
            <ArrowBackIcon fontSize="large" />
          </Button>
          {/* </Link> */}
          <Typography variant="h6" color="inherit" noWrap>
            Section List
          </Typography>
        </Toolbar>
      </AppBar>
      <main>
        {/* Hero unit */}
        <div className={classes.heroContent}>
          <Container maxWidth="sm">
            <Typography
              component="h1"
              variant="h2"
              align="center"
              color="textPrimary"
              gutterBottom
            >
              Section List
            </Typography>
            <Typography
              variant="h5"
              align="center"
              color="textSecondary"
              paragraph
            >
              App allows to manage all sections in restaurant.
            </Typography>

            <ColorButton
              type="submit"
              fullWidth
              variant="contained"
              color="primary"
              className={classes.submit}
            >
              Add section
            </ColorButton>
          </Container>
        </div>
        <Container className={classes.cardGrid} maxWidth="md">
          {/* End hero unit */}
          <Grid container spacing={12}>
            {sections.map((section) => (
              <Grid item key={section.id} xs={12} sm={6} md={4}>
                <Card className={classes.card}>
                  <CardContent className={classes.cardContent}>
                    <Typography gutterBottom variant="h4" component="h2">
                      Section:
                    </Typography>
                    <Typography gutterBottom variant="h3" component="h2">
                      {section.name}
                    </Typography>
                  </CardContent>
                  <CardActions>
                    <ColorButton
                      type="submit"
                      fullWidth
                      variant="contained"
                      color="primary"
                      className={classes.submit}
                    >
                      Delete section
                    </ColorButton>
                  </CardActions>
                  <CardActions>
                    <ColorButton
                      type="submit"
                      fullWidth
                      variant="contained"
                      color="primary"
                      className={classes.submit}
                    >
                      Add dish
                    </ColorButton>
                  </CardActions>
                  {section.positions !== null ? (
                    section.positions.map((dish) => (
                      <Grid item key={dish.id} xs={12} sm={6} md={4}>
                        <Card className={classes.card}>
                          <CardContent className={classes.cardContent}>
                            <Typography
                              gutterBottom
                              variant="h5"
                              component="h2"
                            >
                              Dish: {dish.name}
                            </Typography>
                          </CardContent>
                        </Card>
                      </Grid>
                    ))
                  ) : (
                    <Card className={classes.card}>
                      <CardContent className={classes.cardContent}>
                        <Typography gutterBottom variant="h5" component="h2">
                          This section has no dishes
                        </Typography>
                      </CardContent>
                    </Card>
                  )}
                </Card>
              </Grid>
            ))}
          </Grid>
        </Container>
      </main>
    </React.Fragment>
  );
}
