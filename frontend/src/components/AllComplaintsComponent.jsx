import { useState, useContext, useEffect } from "react";
import SnackbarContext from '../contexts/SnackbarContext';
import LoadingContext from '../contexts/LoadingContext';
import axios from 'axios';


export default function AllComplaints(props){
    
    const restaurantId = 1;

    const { setLoading } = useContext(LoadingContext);
    const { setSnackbar } = useContext(SnackbarContext);
    const [complaints, setComplaints] = useState([]);

    useEffect(() =>{
        async function fetchData(){
            
            setLoading(true);

            var config = {
                method: 'get',
                url: 'https://localhost:44384/restaurant/complaint/all?id=' + restaurantId.toString(),
                headers:{
                    'security-header': 'some-token'
                }
            }

            try
            {
                const response = await axios(config);
                console.log(response);
                setComplaints(response.data);
            }
            catch(e)
            {
                console.error(e);
                setSnackbar({
                    open: true,
                    messafe: 'Error occured',
                    type: 'error'
                })
            }
            setLoading(false);
        }

        fetchData();

    }, [setComplaints, setLoading, setSnackbar]);

    return(
        <div>
            Complaints
            {complaints}
        </div>
    )
}