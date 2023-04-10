var connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub")
    .build();