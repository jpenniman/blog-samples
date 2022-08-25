var keyBytes = File.ReadAllBytes("aes.key");
var crypto = new CryptoLib.Crypto(keyBytes);

for (var i = 0; i < 10; i++)
{
    var secure = crypto.Encrypt("Foobar");
    Console.WriteLine(Convert.ToBase64String(secure));
    Console.WriteLine(crypto.Decrypt(secure));
}
Console.ReadLine();