function authHeader(user){
    return {
        'Accept': 'application/json, text/plain',
        'Content-Type': 'application/json;charset=UTF-8',
        'Authorization': 'Bearer ' + user.token
      };
}

export default authHeader;