#if USE_RESTRICTED_FEATURES
#else
using System.Text;
#endif

namespace NetTools
{
    public enum HashVariant
    {
        SHA1,
        SHA3_512,
    }

    public class Hash
    {
        public static HashVariant Variant { get; set; } = HashVariant.SHA3_512;
        // Declare this in SecretUuid.cs:
        // public static readonly byte[] GuidBytes = new byte[16] { <your own guid> };
        // public static readonly System.Guid Guid = new System.Guid(GuidBytes);
        public static string Suffix { get; } = SecretUuid.Guid.ToString("N");

        public static string Convert(string text)
        {
            text += Suffix;

            switch (Variant)
            {
                default:
                case HashVariant.SHA1:
                    return SHA1(text);
                case HashVariant.SHA3_512:
                    return SHA3_512(text);
            }
        }

        #region SHA1
#if USE_RESTRICTED_FEATURES
        private static bool SHA1_JSLibraryHasBeenLoaded;

        public static string SHA1(string data)
        {
            // First, load the SHA1 JS library if it has not been already loaded:
            if (!SHA1_JSLibraryHasBeenLoaded)
            {
                SHA1_JSLibraryHasBeenLoaded = true;

                CSHTML5.Interop.ExecuteJavaScript(@"
/* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -  */
/*  SHA-1 implementation in JavaScript                  (c) Chris Veness 2002-2014 / MIT Licence  */
/*                                                                                                */
/*  - see http://csrc.nist.gov/groups/ST/toolkit/secure_hashing.html                              */
/*        http://csrc.nist.gov/groups/ST/toolkit/examples.html                                    */
/* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -  */

/* jshint node:true *//* global define, escape, unescape */
//'use strict';


/**
 * SHA-1 hash function reference implementation.
 *
 * @namespace
 */
if (!document.CSHTML5)
    document.CSHTML5 = {};
if (!document.CSHTML5.Extensions)
    document.CSHTML5.Extensions = {};
if (!document.CSHTML5.Extensions.Sha1)
    document.CSHTML5.Extensions.Sha1 = {};


/**
 * Generates SHA-1 hash of string.
 *
 * @param   {string} msg - (Unicode) string to be hashed.
 * @returns {string} Hash of msg as hex character string.
 */
document.CSHTML5.Extensions.Sha1.hash = function(msg) {
    // convert string to UTF-8, as SHA only deals with byte-streams
    msg = msg.utf8Encode();

    // constants [§4.2.1]
    var K = [ 0x5a827999, 0x6ed9eba1, 0x8f1bbcdc, 0xca62c1d6 ];

    // PREPROCESSING

    msg += String.fromCharCode(0x80);  // add trailing '1' bit (+ 0's padding) to string [§5.1.1]

    // convert string msg into 512-bit/16-integer blocks arrays of ints [§5.2.1]
    var l = msg.length/4 + 2; // length (in 32-bit integers) of msg + ‘1’ + appended length
    var N = Math.ceil(l/16);  // number of 16-integer-blocks required to hold 'l' ints
    var M = new Array(N);

    for (var i=0; i<N; i++) {
        M[i] = new Array(16);
        for (var j=0; j<16; j++) {  // encode 4 chars per integer, big-endian encoding
            M[i][j] = (msg.charCodeAt(i*64+j*4)<<24) | (msg.charCodeAt(i*64+j*4+1)<<16) |
                (msg.charCodeAt(i*64+j*4+2)<<8) | (msg.charCodeAt(i*64+j*4+3));
        } // note running off the end of msg is ok 'cos bitwise ops on NaN return 0
    }
    // add length (in bits) into final pair of 32-bit integers (big-endian) [§5.1.1]
    // note: most significant word would be (len-1)*8 >>> 32, but since JS converts
    // bitwise-op args to 32 bits, we need to simulate this by arithmetic operators
    M[N-1][14] = ((msg.length-1)*8) / Math.pow(2, 32); M[N-1][14] = Math.floor(M[N-1][14]);
    M[N-1][15] = ((msg.length-1)*8) & 0xffffffff;

    // set initial hash value [§5.3.1]
    var H0 = 0x67452301;
    var H1 = 0xefcdab89;
    var H2 = 0x98badcfe;
    var H3 = 0x10325476;
    var H4 = 0xc3d2e1f0;

    // HASH COMPUTATION [§6.1.2]

    var W = new Array(80); var a, b, c, d, e;
    for (var i=0; i<N; i++) {

        // 1 - prepare message schedule 'W'
        for (var t=0;  t<16; t++) W[t] = M[i][t];
        for (var t=16; t<80; t++) W[t] = document.CSHTML5.Extensions.Sha1.ROTL(W[t-3] ^ W[t-8] ^ W[t-14] ^ W[t-16], 1);

        // 2 - initialise five working variables a, b, c, d, e with previous hash value
        a = H0; b = H1; c = H2; d = H3; e = H4;

        // 3 - main loop
        for (var t=0; t<80; t++) {
            var s = Math.floor(t/20); // seq for blocks of 'f' functions and 'K' constants
            var T = (document.CSHTML5.Extensions.Sha1.ROTL(a,5) + document.CSHTML5.Extensions.Sha1.f(s,b,c,d) + e + K[s] + W[t]) & 0xffffffff;
            e = d;
            d = c;
            c = document.CSHTML5.Extensions.Sha1.ROTL(b, 30);
            b = a;
            a = T;
        }

        // 4 - compute the new intermediate hash value (note 'addition modulo 2^32')
        H0 = (H0+a) & 0xffffffff;
        H1 = (H1+b) & 0xffffffff;
        H2 = (H2+c) & 0xffffffff;
        H3 = (H3+d) & 0xffffffff;
        H4 = (H4+e) & 0xffffffff;
    }

    return document.CSHTML5.Extensions.Sha1.toHexStr(H0) + document.CSHTML5.Extensions.Sha1.toHexStr(H1) + document.CSHTML5.Extensions.Sha1.toHexStr(H2) +
           document.CSHTML5.Extensions.Sha1.toHexStr(H3) + document.CSHTML5.Extensions.Sha1.toHexStr(H4);
};


/**
 * Function 'f' [§4.1.1].
 * @private
 */
document.CSHTML5.Extensions.Sha1.f = function(s, x, y, z)  {
    switch (s) {
        case 0: return (x & y) ^ (~x & z);           // Ch()
        case 1: return  x ^ y  ^  z;                 // Parity()
        case 2: return (x & y) ^ (x & z) ^ (y & z);  // Maj()
        case 3: return  x ^ y  ^  z;                 // Parity()
    }
};

/**
 * Rotates left (circular left shift) value x by n positions [§3.2.5].
 * @private
 */
document.CSHTML5.Extensions.Sha1.ROTL = function(x, n) {
    return (x<<n) | (x>>>(32-n));
};


/**
 * Hexadecimal representation of a number.
 * @private
 */
document.CSHTML5.Extensions.Sha1.toHexStr = function(n) {
    // note can't use toString(16) as it is implementation-dependant,
    // and in IE returns signed numbers when used on full words
    var s="""", v;
    for (var i=7; i>=0; i--) { v = (n>>>(i*4)) & 0xf; s += v.toString(16); }
    return s;
};


/* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -  */


/** Extend String object with method to encode multi-byte string to utf8
 *  - monsur.hossa.in/2012/07/20/utf-8-in-javascript.html */
if (typeof String.prototype.utf8Encode == 'undefined') {
    String.prototype.utf8Encode = function() {
        return unescape( encodeURIComponent( this ) );
    };
}

/** Extend String object with method to decode utf8 string to multi-byte */
if (typeof String.prototype.utf8Decode == 'undefined') {
    String.prototype.utf8Decode = function() {
        try {
            return decodeURIComponent( escape( this ) );
        } catch (e) {
            return this; // invalid UTF-8? return as-is
        }
    };
}


/* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -  */
if (typeof module != 'undefined' && module.exports) module.exports = document.CSHTML5.Extensions.Sha1; // CommonJs export
if (typeof define == 'function' && define.amd) define([], function() { return document.CSHTML5.Extensions.Sha1; }); // AMD
");
            }

            // Then do the actual calculation:
            string result = System.Convert.ToString(CSHTML5.Interop.ExecuteJavaScript(@"document.CSHTML5.Extensions.Sha1.hash($0)", data));

            return result;
        }
#else
        public static string SHA1(string data)
        {
            return "abcdef000" + System.Convert.ToBase64String(Encoding.UTF8.GetBytes(data));
        }
#endif
        #endregion

        #region SHA3
#if USE_RESTRICTED_FEATURES
        private static bool SHA3_512_JSLibraryHasBeenLoaded;

        public static string SHA3_512(string data)
        {
            // First, load the SHA1 JS library if it has not been already loaded:
            if (!SHA3_512_JSLibraryHasBeenLoaded)
            {
                SHA3_512_JSLibraryHasBeenLoaded = true;

                CSHTML5.Interop.ExecuteJavaScript(
"/*"+
" A JavaScript implementation of the SHA family of hashes, as"+
" defined in FIPS PUB 180-4 and FIPS PUB 202, as well as the corresponding"+
" HMAC implementation as defined in FIPS PUB 198a"+
"\n"+
" Copyright 2008-2018 Brian Turek, 1998-2009 Paul Johnston & Contributors"+
" Distributed under the BSD License"+
" See http://caligatio.github.com/jsSHA/ for more information"+
"*/"+
"/* jshint node:true *//* global define, escape, unescape */" +
"//'use strict';" +
"\n" +
"/**" +
" * SHA-3 hash function implementation."+
" *"+
" * @namespace"+
" */"+
"if (!document.CSHTML5)"+
"    document.CSHTML5 = {};"+
"if (!document.CSHTML5.Extensions)"+
"    document.CSHTML5.Extensions = {};"+
"if (!document.CSHTML5.Extensions.Sha3)"+
"    document.CSHTML5.Extensions.Sha3 = {};"+
"\n" +
"(function(L){function u(d,b,h){var c=0,a=[],l=0,e,m,r,g,k,f,p,v,A=!1,n=[],u=[],w,z=!1,x=!1,t=-1;h=h||{};e=h.encoding||\"UTF8\";w=h.numRounds||1;if(w!==parseInt(w,10)||1>w)throw Error(\"numRounds must a integer >= 1\");if(0===d.lastIndexOf(\"SHA3-\",0)||0===d.lastIndexOf(\"SHAKE\",0)){var C=6;f=B;v=function(c){var a=[],e;for(e=0;5>e;e+=1)a[e]=c[e].slice();return a};t=1;if(\"SHA3-224\"===d)k=1152,g=224;else if(\"SHA3-256\"===d)k=1088,g=256;else if(\"SHA3-384\"===d)k=832,g=384;else if(\"SHA3-512\"===d)k=" +
"576,g=512;else if(\"SHAKE128\"===d)k=1344,g=-1,C=31,x=!0;else if(\"SHAKE256\"===d)k=1088,g=-1,C=31,x=!0;else throw Error(\"Chosen SHA variant is not supported\");p=function(c,a,e,g,d){e=k;var b=C,m,l=[],f=e>>>5,h=0,r=a>>>5;for(m=0;m<r&&a>=e;m+=f)g=B(c.slice(m,m+f),g),a-=e;c=c.slice(m);for(a%=e;c.length<f;)c.push(0);m=a>>>3;c[m>>2]^=b<<m%4*8;c[f-1]^=2147483648;for(g=B(c,g);32*l.length<d;){c=g[h%5][h/5|0];l.push(c.b);if(32*l.length>=d)break;l.push(c.a);h+=1;0===64*h%e&&B(null,g)}return l}}else throw Error(\"Chosen SHA variant is not supported\");"+
"r=D(b,e,t);m=y(d);this.setHMACKey=function(a,b,l){var h;if(!0===A)throw Error(\"HMAC key already set\");if(!0===z)throw Error(\"Cannot set HMAC key after calling update\");if(!0===x)throw Error(\"SHAKE is not supported for HMAC\");e=(l||{}).encoding||\"UTF8\";b=D(b,e,t)(a);a=b.binLen;b=b.value;h=k>>>3;l=h/4-1;if(h<a/8){for(b=p(b,a,0,y(d),g);b.length<=l;)b.push(0);b[l]&=4294967040}else if(h>a/8){for(;b.length<=l;)b.push(0);b[l]&=4294967040}for(a=0;a<=l;a+=1)n[a]=b[a]^909522486,u[a]=b[a]^1549556828;m=f(n,m);"+
"c=k;A=!0};this.update=function(e){var b,g,d,h=0,p=k>>>5;b=r(e,a,l);e=b.binLen;g=b.value;b=e>>>5;for(d=0;d<b;d+=p)h+k<=e&&(m=f(g.slice(d,d+p),m),h+=k);c+=h;a=g.slice(h>>>5);l=e%k;z=!0};this.getHash=function(e,b){var h,f,r,k;if(!0===A)throw Error(\"Cannot call getHash after setting HMAC key\");r=E(b);if(!0===x){if(-1===r.shakeLen)throw Error(\"shakeLen must be specified in options\");g=r.shakeLen}switch(e){case \"HEX\":h=function(a){return F(a,g,t,r)};break;case \"B64\":h=function(a){return G(a,g,t,r)};break;"+
"case \"BYTES\":h=function(a){return H(a,g,t)};break;case \"ARRAYBUFFER\":try{f=new ArrayBuffer(0)}catch(q){throw Error(\"ARRAYBUFFER not supported by this environment\");}h=function(a){return I(a,g,t)};break;default:throw Error(\"format must be HEX, B64, BYTES, or ARRAYBUFFER\");}k=p(a.slice(),l,c,v(m),g);for(f=1;f<w;f+=1)!0===x&&0!==g%32&&(k[k.length-1]&=16777215>>>24-g%32),k=p(k,g,0,y(d),g);return h(k)};this.getHMAC=function(e,b){var h,r,n,w;if(!1===A)throw Error(\"Cannot call getHMAC without first setting HMAC key\");"+
"n=E(b);switch(e){case \"HEX\":h=function(a){return F(a,g,t,n)};break;case \"B64\":h=function(a){return G(a,g,t,n)};break;case \"BYTES\":h=function(a){return H(a,g,t)};break;case \"ARRAYBUFFER\":try{h=new ArrayBuffer(0)}catch(M){throw Error(\"ARRAYBUFFER not supported by this environment\");}h=function(a){return I(a,g,t)};break;default:throw Error(\"outputFormat must be HEX, B64, BYTES, or ARRAYBUFFER\");}r=p(a.slice(),l,c,v(m),g);w=f(u,y(d));w=p(r,g,k,w,g);return h(w)}}function f(d,b){this.a=d;this.b=b}function F(d,"+
"b,h,c){var a=\"\";b/=8;var l,e,m;m=-1===h?3:0;for(l=0;l<b;l+=1)e=d[l>>>2]>>>8*(m+l%4*h),a+=\"0123456789abcdef\".charAt(e>>>4&15)+\"0123456789abcdef\".charAt(e&15);return c.outputUpper?a.toUpperCase():a}function G(d,b,h,c){var a=\"\",l=b/8,e,m,f,g;g=-1===h?3:0;for(e=0;e<l;e+=3)for(m=e+1<l?d[e+1>>>2]:0,f=e+2<l?d[e+2>>>2]:0,f=(d[e>>>2]>>>8*(g+e%4*h)&255)<<16|(m>>>8*(g+(e+1)%4*h)&255)<<8|f>>>8*(g+(e+2)%4*h)&255,m=0;4>m;m+=1)8*e+6*m<=b?a+=\"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/\".charAt(f>>>"+
"6*(3-m)&63):a+=c.b64Pad;return a}function H(d,b,h){var c=\"\";b/=8;var a,l,e;e=-1===h?3:0;for(a=0;a<b;a+=1)l=d[a>>>2]>>>8*(e+a%4*h)&255,c+=String.fromCharCode(l);return c}function I(d,b,h){b/=8;var c,a=new ArrayBuffer(b),l,e;e=new Uint8Array(a);l=-1===h?3:0;for(c=0;c<b;c+=1)e[c]=d[c>>>2]>>>8*(l+c%4*h)&255;return a}function E(d){var b={outputUpper:!1,b64Pad:\"=\",shakeLen:-1};d=d||{};b.outputUpper=d.outputUpper||!1;!0===d.hasOwnProperty(\"b64Pad\")&&(b.b64Pad=d.b64Pad);if(!0===d.hasOwnProperty(\"shakeLen\")){if(0!=="+
"d.shakeLen%8)throw Error(\"shakeLen must be a multiple of 8\");b.shakeLen=d.shakeLen}if(\"boolean\"!==typeof b.outputUpper)throw Error(\"Invalid outputUpper formatting option\");if(\"string\"!==typeof b.b64Pad)throw Error(\"Invalid b64Pad formatting option\");return b}function D(d,b,h){switch(b){case \"UTF8\":case \"UTF16BE\":case \"UTF16LE\":break;default:throw Error(\"encoding must be UTF8, UTF16BE, or UTF16LE\");}switch(d){case \"HEX\":d=function(c,a,b){var e=c.length,d,f,g,k,q,p;if(0!==e%2)throw Error(\"String of HEX type must be in byte increments\");"+
"a=a||[0];b=b||0;q=b>>>3;p=-1===h?3:0;for(d=0;d<e;d+=2){f=parseInt(c.substr(d,2),16);if(isNaN(f))throw Error(\"String of HEX type contains invalid characters\");k=(d>>>1)+q;for(g=k>>>2;a.length<=g;)a.push(0);a[g]|=f<<8*(p+k%4*h)}return{value:a,binLen:4*e+b}};break;case \"TEXT\":d=function(c,a,d){var e,m,f=0,g,k,q,p,v,n;a=a||[0];d=d||0;q=d>>>3;if(\"UTF8\"===b)for(n=-1===h?3:0,g=0;g<c.length;g+=1)for(e=c.charCodeAt(g),m=[],128>e?m.push(e):2048>e?(m.push(192|e>>>6),m.push(128|e&63)):55296>e||57344<=e?m.push(224|"+
"e>>>12,128|e>>>6&63,128|e&63):(g+=1,e=65536+((e&1023)<<10|c.charCodeAt(g)&1023),m.push(240|e>>>18,128|e>>>12&63,128|e>>>6&63,128|e&63)),k=0;k<m.length;k+=1){v=f+q;for(p=v>>>2;a.length<=p;)a.push(0);a[p]|=m[k]<<8*(n+v%4*h);f+=1}else if(\"UTF16BE\"===b||\"UTF16LE\"===b)for(n=-1===h?2:0,m=\"UTF16LE\"===b&&1!==h||\"UTF16LE\"!==b&&1===h,g=0;g<c.length;g+=1){e=c.charCodeAt(g);!0===m&&(k=e&255,e=k<<8|e>>>8);v=f+q;for(p=v>>>2;a.length<=p;)a.push(0);a[p]|=e<<8*(n+v%4*h);f+=2}return{value:a,binLen:8*f+d}};break;case \"B64\":d="+
"function(c,a,b){var e=0,d,f,g,k,q,p,n,u;if(-1===c.search(/^[a-zA-Z0-9=+\\/]+$/))throw Error(\"Invalid character in base-64 string\");f=c.indexOf(\"=\");c=c.replace(/\\=/g,\"\");if(-1!==f&&f<c.length)throw Error(\"Invalid '=' found in base-64 string\");a=a||[0];b=b||0;p=b>>>3;u=-1===h?3:0;for(f=0;f<c.length;f+=4){q=c.substr(f,4);for(g=k=0;g<q.length;g+=1)d=\"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/\".indexOf(q[g]),k|=d<<18-6*g;for(g=0;g<q.length-1;g+=1){n=e+p;for(d=n>>>2;a.length<=d;)a.push(0);"+
"a[d]|=(k>>>16-8*g&255)<<8*(u+n%4*h);e+=1}}return{value:a,binLen:8*e+b}};break;case \"BYTES\":d=function(c,a,b){var e,d,f,g,k,n;a=a||[0];b=b||0;f=b>>>3;n=-1===h?3:0;for(d=0;d<c.length;d+=1)e=c.charCodeAt(d),k=d+f,g=k>>>2,a.length<=g&&a.push(0),a[g]|=e<<8*(n+k%4*h);return{value:a,binLen:8*c.length+b}};break;case \"ARRAYBUFFER\":try{d=new ArrayBuffer(0)}catch(c){throw Error(\"ARRAYBUFFER not supported by this environment\");}d=function(c,a,b){var d,f,n,g,k,q;a=a||[0];b=b||0;f=b>>>3;k=-1===h?3:0;q=new Uint8Array(c);"+
"for(d=0;d<c.byteLength;d+=1)g=d+f,n=g>>>2,a.length<=n&&a.push(0),a[n]|=q[d]<<8*(k+g%4*h);return{value:a,binLen:8*c.byteLength+b}};break;default:throw Error(\"format must be HEX, TEXT, B64, BYTES, or ARRAYBUFFER\");}return d}function z(d,b){return 32<b?(b-=32,new f(d.b<<b|d.a>>>32-b,d.a<<b|d.b>>>32-b)):0!==b?new f(d.a<<b|d.b>>>32-b,d.b<<b|d.a>>>32-b):d}function n(d,b){return new f(d.a^b.a,d.b^b.b)}function y(d){var b=[];if(0===d.lastIndexOf(\"SHA3-\",0)||0===d.lastIndexOf(\"SHAKE\",0))for(d=0;5>d;d+=1)b[d]="+
"[new f(0,0),new f(0,0),new f(0,0),new f(0,0),new f(0,0)];else throw Error(\"No SHA variants supported\");return b}function B(d,b){var h,c,a,l,e=[],m=[];if(null!==d)for(c=0;c<d.length;c+=2)b[(c>>>1)%5][(c>>>1)/5|0]=n(b[(c>>>1)%5][(c>>>1)/5|0],new f(d[c+1],d[c]));for(h=0;24>h;h+=1){l=y(\"SHA3-\");for(c=0;5>c;c+=1){a=b[c][0];var r=b[c][1],g=b[c][2],k=b[c][3],q=b[c][4];e[c]=new f(a.a^r.a^g.a^k.a^q.a,a.b^r.b^g.b^k.b^q.b)}for(c=0;5>c;c+=1)m[c]=n(e[(c+4)%5],z(e[(c+1)%5],1));for(c=0;5>c;c+=1)for(a=0;5>a;a+=1)b[c][a]="+
"n(b[c][a],m[c]);for(c=0;5>c;c+=1)for(a=0;5>a;a+=1)l[a][(2*c+3*a)%5]=z(b[c][a],J[c][a]);for(c=0;5>c;c+=1)for(a=0;5>a;a+=1)b[c][a]=n(l[c][a],new f(~l[(c+1)%5][a].a&l[(c+2)%5][a].a,~l[(c+1)%5][a].b&l[(c+2)%5][a].b));b[0][0]=n(b[0][0],K[h])}return b}var J,K;K=[new f(0,1),new f(0,32898),new f(2147483648,32906),new f(2147483648,2147516416),new f(0,32907),new f(0,2147483649),new f(2147483648,2147516545),new f(2147483648,32777),new f(0,138),new f(0,136),new f(0,2147516425),new f(0,2147483658),new f(0,2147516555),"+
"new f(2147483648,139),new f(2147483648,32905),new f(2147483648,32771),new f(2147483648,32770),new f(2147483648,128),new f(0,32778),new f(2147483648,2147483658),new f(2147483648,2147516545),new f(2147483648,32896),new f(0,2147483649),new f(2147483648,2147516424)];J=[[0,36,3,41,18],[1,44,10,45,2],[62,6,43,15,61],[28,55,25,21,56],[27,20,39,8,14]];\"function\"===typeof define&&define.amd?define(function(){return u}):\"undefined\"!==typeof exports?(\"undefined\"!==typeof module&&module.exports&&(module.exports="+
"u),exports=u):L.jsSHA=u})(this);"+
"\n" +
"document.CSHTML5.Extensions.Sha3.hash = function(msg) {" +
"var shaObj = new jsSHA(\"SHA3-512\", \"TEXT\");" +
"shaObj.update(msg);" +
"return shaObj.getHash(\"HEX\");"+
/*"return msg;"+*/
"}" +
"\n" +
"/* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -  */" +
"if (typeof module != 'undefined' && module.exports) module.exports = document.CSHTML5.Extensions.Sha3;" +
"if (typeof define == 'function' && define.amd) define([], function() { return document.CSHTML5.Extensions.Sha3; });" +
""
);
            }

            // Then do the actual calculation:
            string result = System.Convert.ToString(CSHTML5.Interop.ExecuteJavaScript(@"document.CSHTML5.Extensions.Sha3.hash($0)", data));

            return result;
        }
#else
        public static string SHA3_512(string data)
        {
            return "abcdef000" + System.Convert.ToBase64String(Encoding.UTF8.GetBytes(data));
        }
#endif
        #endregion
    }
}
