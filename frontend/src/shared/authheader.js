function authHeader(user){
    return {
        apiKey: user.apiKey 
    };
}

export default authHeader;