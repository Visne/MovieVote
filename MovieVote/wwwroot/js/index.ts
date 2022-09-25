const ENDPOINT: string = `${location.protocol.replace(/^http/, "ws")}//${location.host}/vote`;
let ws: WebSocket = new WebSocket(ENDPOINT);

ws.onmessage = (e: MessageEvent) => {
    console.log(e.data)
    let { m: movieId, u: isUpvote, v: newCount, s: isUs }: { m: number, u: boolean, v: number, s: boolean } = JSON.parse(e.data);
    
    const movie: HTMLElement | null = document.getElementById(movieId.toString());
    const vote = movie?.querySelector(".upvote");
    const counter: HTMLElement | null = vote?.querySelector(".upvote-counter") ?? null;
    const button: HTMLImageElement | null = vote?.querySelector(".upvote-button") ?? null;
    
    // Movie is not on this page
    if (counter === null || button === null) return;
    
    console.log("setting count to " + newCount);
    setCounter(counter, newCount);
    
    if (isUs && isUpvote) {
        button.classList.add("upvoted");
    } else if (isUs && !isUpvote) {
        button.classList.remove("upvoted");
    }
    
    return false;
}

async function upvote(movie: HTMLDivElement): Promise<void> {
    const vote: HTMLDivElement | null = movie.querySelector(".upvote");
    const counter: HTMLDivElement | null = vote?.querySelector(".upvote-counter") ?? null;
    const button: HTMLImageElement | null = vote?.querySelector(".upvote-button") ?? null;
    const id = movie.id;

    if (!counter || !button) {
        console.log("Button or counter not found.");
        return;
    }
    
    // TODO: Add toast
    switch (ws.readyState) {
        case WebSocket.CONNECTING:
            console.log("WebSocket is still connecting, please retry.");
            return;
        case WebSocket.CLOSING:
        case WebSocket.CLOSED:
            console.log("WebSocket is closed, please reload the page.");
            return;
    }
    
    let sessionId: string | null = getCookie("session");
    
    // TODO: Add toast
    if (sessionId == null) {
        console.log("Session cookies not set, can't upvote.");
        return;
    }
    
    let msg = {
        m: id, // Movie ID
        s: sessionId, // Session ID
    }

    if (!button.classList.contains("upvoted")) {
        button.classList.add("upvoted");
        incrementCounter(counter);
        ws.send(JSON.stringify({ u: true, ...msg }));
    } else {
        button.classList.remove("upvoted");
        decrementCounter(counter);
        ws.send(JSON.stringify({ u: false, ...msg }));
    }
}

function setCounter(counter: HTMLElement, newCount: number): void {
    counter.textContent = Math.max(0, newCount).toString();
}

function incrementCounter(counter: HTMLElement): void {
    counter.textContent = Math.max(1, (parseInt(counter.textContent ?? "0") + 1)).toString();
}

function decrementCounter(counter: HTMLElement): void {
    counter.textContent = Math.max(0, (parseInt(counter.textContent ?? "0") - 1)).toString();
}

function getCookie(name: string): string | null {
    const match = document.cookie.match(RegExp(`(?:^|;\\s*)${name}=([^;]*)`));
    return match ? match[1] : null;
}

function toggleOpen(element: HTMLDivElement): void {
    element.classList.toggle('closed');
}