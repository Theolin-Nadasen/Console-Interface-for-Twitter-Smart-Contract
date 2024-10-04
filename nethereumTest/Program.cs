using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using nethereumTest;
using System;

// account stuff
// put private key here
var privatekey = "0x";
var account = new Account(privatekey);

// rpc - blockchain
string url = "https://polygon-zkevm-cardona.blockpi.network/v1/rpc/public";

// web3 instance
var web3 = new Web3(account, url);

Console.WriteLine("connected to polygon zkEVM chain");

// contract address and ABI (Application binary interface)
var contractAddr = "0x4221b6fcEb6623490D3B513d7864268191432C11";
var ABI = "[{\"inputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"constructor\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"id\",\"type\":\"uint256\"},{\"indexed\":false,\"internalType\":\"address\",\"name\":\"author\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"string\",\"name\":\"content\",\"type\":\"string\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"timestamp\",\"type\":\"uint256\"}],\"name\":\"tweetCreated\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":false,\"internalType\":\"address\",\"name\":\"liker\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"address\",\"name\":\"tweetAuthor\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"id\",\"type\":\"uint256\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"newLikeCount\",\"type\":\"uint256\"}],\"name\":\"tweetLiked\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":false,\"internalType\":\"address\",\"name\":\"unliker\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"address\",\"name\":\"tweetAuthor\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"id\",\"type\":\"uint256\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"newLikeCount\",\"type\":\"uint256\"}],\"name\":\"tweetUnLiked\",\"type\":\"event\"},{\"inputs\":[{\"internalType\":\"string\",\"name\":\"_msg\",\"type\":\"string\"}],\"name\":\"createTweet\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"_owner\",\"type\":\"address\"}],\"name\":\"getAllTweets\",\"outputs\":[{\"components\":[{\"internalType\":\"uint256\",\"name\":\"id\",\"type\":\"uint256\"},{\"internalType\":\"address\",\"name\":\"author\",\"type\":\"address\"},{\"internalType\":\"string\",\"name\":\"content\",\"type\":\"string\"},{\"internalType\":\"uint256\",\"name\":\"timestamp\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"likes\",\"type\":\"uint256\"}],\"internalType\":\"struct twitter.tweet[]\",\"name\":\"\",\"type\":\"tuple[]\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"_i\",\"type\":\"uint256\"}],\"name\":\"getTweet\",\"outputs\":[{\"components\":[{\"internalType\":\"uint256\",\"name\":\"id\",\"type\":\"uint256\"},{\"internalType\":\"address\",\"name\":\"author\",\"type\":\"address\"},{\"internalType\":\"string\",\"name\":\"content\",\"type\":\"string\"},{\"internalType\":\"uint256\",\"name\":\"timestamp\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"likes\",\"type\":\"uint256\"}],\"internalType\":\"struct twitter.tweet\",\"name\":\"\",\"type\":\"tuple\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"author\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"id\",\"type\":\"uint256\"}],\"name\":\"likeTweet\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"owner\",\"outputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"author\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"id\",\"type\":\"uint256\"}],\"name\":\"unlikeTweet\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"}]";

// get the contract from the chain
var contract = web3.Eth.GetContract(ABI, contractAddr);
Console.WriteLine("contract loaded");


// important or it will give strange errors
web3.TransactionManager.UseLegacyAsDefault = true;

var newTweet = new createTweet() { msg = "this tweet came from c#" };


var createTweetHandle = web3.Eth.GetContractTransactionHandler<createTweet>();
var reciept = await createTweetHandle.SendRequestAndWaitForReceiptAsync(contractAddr, newTweet);

Console.WriteLine($"transaction hash : {reciept.TransactionHash}");

// get all the tweets
var getAllTweets = contract.GetFunction("getAllTweets");
var result = await getAllTweets.CallAsync<List<tweet>>("0xf136d28ea0600a1A648f7D1537ce09fAa932746a");

foreach(tweet tweet in result)
{
    Console.WriteLine(tweet.content);
}
